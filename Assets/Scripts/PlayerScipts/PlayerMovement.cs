using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        ticksSinceStart++;

        rb.gravityScale = baseGravity + ticksSinceStart * 0.00001f;

        float terminalVelocity = 3 * Mathf.Sqrt(rb.gravityScale);
        rb.linearVelocityY = Mathf.Clamp(rb.linearVelocityY, -terminalVelocity, terminalVelocity);

        AndroidMove();
        PcMove();
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

    public void AndroidMove()
    {
        var touches = new List<Touch>(Input.touchCount);

        for (int i=0;i<touches.Count; i++)
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
