using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public static readonly float MAX_GRAVITY = 1.5f;
    public static readonly float HOLD_SPEED_MULTIPLIER = 1;
    public static readonly float MAX_SPEED_WITHOUT_HOLD = 4;
    public static readonly Func<float, float> HOLDING_SPEED_CURVE = SigmoidFactory(2.5f, 0.01f, 0.3f);

    private Rigidbody2D rb;
    public static float speedmultiplier;
    private static float baseGravity;
    private static float baseSpeed;
    private static int ticksSinceStart;
    private static int rightHoldingTime;
    private static int leftHoldingTime;
    private static InputDevice inputDevice;
    private static PlatformScript currentPlatform;

    private float ScreenWidth;

    private enum InputDevice
    {
        DontKnow, Screen, Keyboard
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        baseGravity = rb.gravityScale;
        ScreenWidth = Screen.width;
    }

    public static void InitializeGame()
    {
        speedmultiplier = 1f;
        baseGravity = 0.35f;
        baseSpeed = 0.5f;
        ticksSinceStart = 0;
        rightHoldingTime = 0;
        leftHoldingTime = 0;
        inputDevice = InputDevice.DontKnow;
        currentPlatform = null;
    }

    void FixedUpdate()
    {
        ticksSinceStart++;

        // Increase gravity scale over time
        rb.gravityScale = baseGravity + Mathf.Pow(ticksSinceStart * 0.0001f, 1.2f);
        rb.gravityScale = Mathf.Min(rb.gravityScale, MAX_GRAVITY);

        // Calculate terminal velocity (accurate physics, i'm not a ctis student)
        float terminalVelocity = 5 * Mathf.Sqrt(rb.gravityScale);

        // Apply more exponential acceleration when falling
        if (rb.linearVelocityY < 0)
        {
            // This multiplier will make the fall feel more exponential
            float fallMultiplier = 1.025f;
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

        // Still clamp to terminal velocity
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -terminalVelocity, float.MaxValue));

        
        if(inputDevice!=InputDevice.Keyboard)
        {
            MobileMove();
        }
        if(inputDevice!=InputDevice.Screen) 
        {
            PcMove();
        }
    }

    public static Func<float, float> SigmoidFactory(float upperBound, float initialValue, float timeToReachHalfOfUpperBound)
    {

        if(upperBound<=initialValue)
        {
            throw new ArgumentException("Upper bound can't be lower than initial value");
        }
        if (initialValue<=0 || timeToReachHalfOfUpperBound <= 0)
        {
            throw new ArgumentException("This function does not allow non-positive arguments");
        }

        float c = upperBound;
        float a = c / initialValue - 1;
        float b = Mathf.Log(a) / timeToReachHalfOfUpperBound;
        return (x) => c / (1 + a * Mathf.Exp(-b * x));
    }

    public void Move(List<Touch> touches)
    {
        float moveSpeed = baseSpeed * Mathf.Clamp(speedmultiplier + 0.0000015f * ticksSinceStart, 1, 20);

        moveSpeed = moveSpeed > MAX_SPEED_WITHOUT_HOLD ? MAX_SPEED_WITHOUT_HOLD : moveSpeed;

        int leftCounter = 0;
        int rightCounter = 0;

        for (int i = 0; i < touches.Count; i++)
        {
            if (touches[i].position.x > ScreenWidth / 2)
            {
                rightCounter++;
            }
            if (touches[i].position.x < ScreenWidth / 2)
            {
                leftCounter++;
            }
        }

        if (rightCounter>leftCounter)
        {
            moveSpeed += HOLDING_SPEED_CURVE.Invoke(rightHoldingTime*0.01f);
            
            rightHoldingTime++;
            leftHoldingTime = 0;
        }
        else if(leftCounter>rightCounter)
        {
            moveSpeed += HOLDING_SPEED_CURVE.Invoke(leftHoldingTime * 0.01f);
            moveSpeed *= -1;
            leftHoldingTime++;
            rightHoldingTime = 0;
        }
        else
        {
            rightHoldingTime = 0;
            leftHoldingTime = 0;
        }

        if (rightCounter!=leftCounter && (Mathf.Sign(moveSpeed) != Mathf.Sign(rb.linearVelocityX) || Mathf.Abs(moveSpeed) > Mathf.Abs(rb.linearVelocityX)))
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }

        if (currentPlatform != null)
        {
            rb.linearVelocityX += 0.25f * currentPlatform.PushAmount();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentPlatform = collision.gameObject.TryGetComponent(out PlatformScript ps) ? ps : null;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        currentPlatform = null;
    }

    public void MobileMove()
    {
        int touchCount = Input.touchCount;
        var touches = new List<Touch>(touchCount);

        for (int i=0;i<touchCount; i++)
        {
            touches.Add(Input.GetTouch(i));
        }

        Move(touches);

        if (touches.Count > 0)
        {
            inputDevice = InputDevice.Screen;
        }
    }

    public void PcMove()
    {
        List<Touch> touches = new();
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            Touch t = new()
            {
                position = new Vector3(ScreenWidth / 2 + 20, 0)
            };
            touches.Add(t);
        }
        if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            Touch t = new()
            {
                position = new Vector3(ScreenWidth / 2 - 20, 0)
            };
            touches.Add(t);
        }
        Move(touches);

        if(touches.Count > 0)
        {
            inputDevice = InputDevice.Keyboard;
        }
    }
}
