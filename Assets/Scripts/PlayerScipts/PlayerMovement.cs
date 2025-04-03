using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myBody;
    public static float MoveSpeed = 0.1f;
    public static float newMoveSpeed = 0.1f;
    public static float gravityScale = 0.1f;
    public static float speedmultiplyer = 0f;
    private float ScreenWidth;
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        gravityScale = myBody.gravityScale;
        ScreenWidth = Screen.width;
    }
    void Update()
    {
        newMoveSpeed += 0.00001f;
        gravityScale += 0.0000005f;
    }
    void FixedUpdate()
    {
        SpeedMultiply();
        AndroidMove();
        PcMove();
    }
    void SpeedMultiply()
    {
        int i = 0;
        if (i < Input.touchCount)
        {
            if (speedmultiplyer < 20f)
            {
                speedmultiplyer += 0.15f;
            }
            else
            {
                speedmultiplyer = 20f;
            }
            ++i;
        }
        else
        {
            speedmultiplyer = 0f;
        }
    }
    public void AndroidMove()
    {
        MoveSpeed = newMoveSpeed * speedmultiplyer;
        int i = 0;
        while (i < Input.touchCount)
        {
            if (Input.GetTouch(i).position.x > ScreenWidth / 2)
            {
                myBody.linearVelocity = new Vector2(MoveSpeed, myBody.linearVelocity.y);
            }
            if (Input.GetTouch(i).position.x < ScreenWidth / 2)
            {
                myBody.linearVelocity = new Vector2(-MoveSpeed, myBody.linearVelocity.y);
            }
            ++i;
        }
    }
    void PcMove()
    {
        MoveSpeed = newMoveSpeed * 10;
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            myBody.linearVelocity = new Vector2(MoveSpeed, myBody.linearVelocity.y);
        }
        if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            myBody.linearVelocity = new Vector2(-MoveSpeed, myBody.linearVelocity.y);
        }
    }
    public void PlatformMove(float x)
    {
        myBody.linearVelocity = new Vector2(x, myBody.linearVelocity.y);
    }
}
