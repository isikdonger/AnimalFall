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
        animFreeze = Owl.GetComponent<Animator>();
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
        if (collisionCount == 0)
        {
            Dissolve();
        }
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
    void Dissolve()
    {
        if (animFreeze.GetCurrentAnimatorStateInfo(0).IsName("OwlFreeze5"))
        {
            Invoke("Dissolve5to4", 0.5f);
        }
        if (animFreeze.GetCurrentAnimatorStateInfo(0).IsName("OwlFreeze4"))
        {
            Invoke("Dissolve4to3", 0.5f);
        }
        if (animFreeze.GetCurrentAnimatorStateInfo(0).IsName("OwlFreeze3"))
        {
            Invoke("Dissolve3to2", 0.5f);
        }
        if (animFreeze.GetCurrentAnimatorStateInfo(0).IsName("OwlFreeze2"))
        {
            Invoke("Dissolve2to1", 0.5f);
        }
        if (animFreeze.GetCurrentAnimatorStateInfo(0).IsName("OwlFreeze1"))
        {
            Invoke("Dissolve1to0", 0.5f);
        }
    }
    void BreakableDeactivate()
    {

        Invoke("DeactivateGameObject", 0.35f);
    }
    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
        //SoundManager.instance.BreakSound();
    }
    void Freeze0to1()
    {
        animFreeze.Play("OwlFreeze1");
    }
    void Freeze1to2()
    {
        animFreeze.Play("OwlFreeze2");
    }
    void Freeze2to3()
    {
        animFreeze.Play("OwlFreeze3");
    }
    void Freeze3to4()
    {
        animFreeze.Play("OwlFreeze4");
    }
    void Freeze4to5()
    {
        animFreeze.Play("OwlFreeze5");
    }
    void Dissolve5to4()
    {
        animFreeze.Play("OwlFreeze4");
    }
    void Dissolve4to3()
    {
        animFreeze.Play("OwlFreeze3");
    }
    void Dissolve3to2()
    {
        animFreeze.Play("OwlFreeze2");
    }
    void Dissolve2to1()
    {
        animFreeze.Play("OwlFreeze1");
    }
    void Dissolve1to0()
    {
        animFreeze.Play("OwlFreeze0");
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.tag== "Player")
        {
            if(is_Beam)
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
    }
    void OnCollisionStay2D(Collision2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            if (is_Freeze)
            {
                if (animFreeze.GetCurrentAnimatorStateInfo(0).IsName("OwlFreeze0"))
                {
                    Invoke("Freeze0to1", 0.5f);
                }
                if (animFreeze.GetCurrentAnimatorStateInfo(0).IsName("OwlFreeze1"))
                {
                    Invoke("Freeze1to2", 0.5f);
                }
                if (animFreeze.GetCurrentAnimatorStateInfo(0).IsName("OwlFreeze2"))
                {
                    Invoke("Freeze2to3", 0.5f);
                }
                if (animFreeze.GetCurrentAnimatorStateInfo(0).IsName("OwlFreeze3"))
                {
                    Invoke("Freeze3to4", 0.5f);
                }
                if (animFreeze.GetCurrentAnimatorStateInfo(0).IsName("OwlFreeze4"))
                {
                    Invoke("Freeze4to5", 0.5f);
                }
            }
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
