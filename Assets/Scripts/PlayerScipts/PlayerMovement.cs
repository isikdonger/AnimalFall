using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public static float speedmultiplyer;
    private static float baseGravity;
    private static float baseSpeed;
    private static int ticksSinceStart;

    private float ScreenWidth;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        baseGravity = rb.gravityScale;
        ScreenWidth = Screen.width;
    }

    public static void InitializeGame()
    {
        speedmultiplyer = 1f;
        baseGravity = 0.5f;
        baseSpeed = 1f;
        ticksSinceStart = 0;
    }

    void FixedUpdate()
    {
        // Increase gravity scale over time
        rb.gravityScale = baseGravity + Mathf.Pow(ticksSinceStart * 0.0001f, 1.2f);

        // Calculate terminal velocity (now quadratic for more exponential feel)
        float terminalVelocity = 5 * rb.gravityScale;

        // Apply more exponential acceleration when falling
        if (rb.linearVelocityY < 0)
        {
            // This multiplier will make the fall feel more exponential
            float fallMultiplier = 1.025f;
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

        // Still clamp to terminal velocity
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -terminalVelocity, float.MaxValue));

        PcMove();
        MobileMove();
    }

    public void Move(List<Touch> touches)
    {
        float moveSpeed = baseSpeed * Mathf.Clamp(speedmultiplyer + 0.0000015f * ticksSinceStart, 1, 20);

        for (int i = 0; i < touches.Count; i++)
        {
            if (touches[i].position.x > ScreenWidth / 2)
            {
                rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
            }
            if (touches[i].position.x < ScreenWidth / 2)
            {
                rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
            }
        }
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
    }

    void PcMove()
    {
        List<Touch> touches = new List<Touch>();
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            Touch t = new Touch();
            t.position = new Vector3(1000, 0);
            touches.Add(t);
        }
        if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            Touch t = new Touch();
            t.position = new Vector3(-1000, 0);
            touches.Add(t);
        }
        Move(touches);
    }

    public void PlatformMove(float x)
    {
        rb.linearVelocity = new Vector2(x, rb.linearVelocity.y);
    }
}
