using UnityEngine;

public class PlatfromScript : MonoBehaviour
{
    private GameObject Owl, BreakablePltatform;
    public static float move_Speed = 1.25f;
    private float bound_Y = 3.1f;
    private int collisionCount;
    public bool is_Breakable, is_Platform, is_Freeze, movingPlatfromLeft, movingPlatfromRight, is_Beam;
    private Animator animBreak, animFreeze;
    void Awake()
    {
        Owl = GameObject.FindGameObjectWithTag("Player");
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
        if (temp.y >= bound_Y)
        {
            gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.tag == "Player")
        {
            if (is_Beam)
            {
                target.transform.position = new Vector2(target.GetComponent<Transform>().position.x, 3f);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D target)
    {
        collisionCount++;
        if (target.gameObject.tag == "Player")
        {
            if (is_Beam)
            {
                //SoundManager.instance.LandSound();
            }
            if (is_Breakable)
            {
                //SoundManager.instance.LandSound();
                animBreak.Play("Break");
            }
            if (is_Freeze)
            {
                //SoundManager.instance.LandSound();
                Debug.Log("Freeze");
                animFreeze.SetTrigger("Freeze");
            }
            if (movingPlatfromLeft)
            {
                //SoundManager.instance.LandSound();
            }
            if (movingPlatfromRight)
            {
                //SoundManager.instance.LandSound();
            }
            if (is_Platform)
            {
                //SoundManager.instance.LandSound();
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        collisionCount--;
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
}
