﻿using UnityEngine;

public class PlatfromScript : MonoBehaviour
{
    private GameObject BreakablePltatform;
    public static float move_Speed = 1.25f;
    public bool is_Breakable, is_Platform, is_Freeze, movingPlatfromLeft, movingPlatfromRight, is_Beam;
    private Animator animBreak, animFreeze;
    void Awake()
    {
        animFreeze = GameObject.Find("FreezeController").GetComponent<Animator>();
        if (is_Breakable)
        {
            BreakablePltatform = GameObject.FindGameObjectWithTag("BreakablePlatform");
            animBreak = BreakablePltatform.GetComponent<Animator>();
        }
    }
    void Update()
    {
        move_Speed += 0.00005f;
        Move();
    }
    void Move()
    {
        Vector2 temp = transform.position;
        temp.y += move_Speed * Time.deltaTime;
        transform.position = temp;
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            if (is_Beam)
            {
                target.transform.position = new Vector2(target.GetComponent<Transform>().position.x, 3f);
            }
        }
        else if (target.gameObject.tag == "TopSpike")
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            //SoundManager.instance.LandSound();
            if (is_Breakable)
            {
                animBreak.Play("Break");
            }
            else if (is_Freeze)
            {
                if (animFreeze.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    animFreeze.SetTrigger("Freeze");
                }
                else
                {
                    animFreeze.speed = 1;
                }
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (is_Freeze)
        {
            animFreeze.speed = -1;
        }
    }
    void OnCollisionStay2D(Collision2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            if (movingPlatfromRight)
            {
                if (ScoreTextScript.scoreValue <= 10)
                {
                    target.gameObject.GetComponent<PlayerMovement>().PlatformMove(0.5f);
                }
                else if (ScoreTextScript.scoreValue <= 20)
                {
                    target.gameObject.GetComponent<PlayerMovement>().PlatformMove(0.7f);
                }
                else if (ScoreTextScript.scoreValue <= 30)
                {
                    target.gameObject.GetComponent<PlayerMovement>().PlatformMove(1f);
                }
                else if (ScoreTextScript.scoreValue > 30)
                {
                    target.gameObject.GetComponent<PlayerMovement>().PlatformMove(1.5f);
                }
            }
            if (movingPlatfromLeft)
            {
                if (ScoreTextScript.scoreValue <= 10)
                {
                    target.gameObject.GetComponent<PlayerMovement>().PlatformMove(-0.5f);
                }
                else if (ScoreTextScript.scoreValue <= 20)
                {
                    target.gameObject.GetComponent<PlayerMovement>().PlatformMove(-0.7f);
                }
                else if (ScoreTextScript.scoreValue <= 30)
                {
                    target.gameObject.GetComponent<PlayerMovement>().PlatformMove(-1f);
                }
                else if (ScoreTextScript.scoreValue > 30)
                {
                    target.gameObject.GetComponent<PlayerMovement>().PlatformMove(-1.5f);
                }
            }
        }
    }

    public void Break()
    {
        if (is_Breakable)
        {
            Destroy(gameObject);
            if (CustomizePanelScript.characterNames[PlayerPrefs.GetInt("characterIndex")].Equals("narwhal"))
            {
                GooglePlayServicesManager.IsAchievementUnlocked("Narwhal Blast", isUnlocked =>
                {
                    if (!isUnlocked)
                    {
                        LocalBackupManager.IncrementBreakCount();
                        if (LocalBackupManager.GetBreakCount() == 10)
                        {
                            GooglePlayServicesManager.UnlockAchievementCoroutine("Narwhal Blast");
                        }
                    }
                });
            }
        }
    }
}
