using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    private Animator anim;
    private float min_X = -2.645f, max_X = 2.645f, min_Y = -4.83f;
    private bool out_of_Bounds;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        CheckBounds();
    }
    void CheckBounds()
    {
        Vector2 temp = transform.position;
        if (temp.x > max_X)
        {
            temp.x = max_X;
        }
        if (temp.x < min_X)
        {
            temp.x = min_X;
        }
        transform.position = temp;
        if (temp.y <= min_Y)
        {
            if (!out_of_Bounds)
            {
                out_of_Bounds = true;
                //SoundManager.instance.DeathSound();
                GameManager.instance.RestartGame();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "TopSpike")
        {
            //SoundManager.instance.DeathSound();
            Destroy(gameObject);
            GameManager.instance.RestartGame();
        }
        if (target.tag ==  "KillCoin")
        {
            //SoundManager.instance.DeathSound();
            Destroy(gameObject);
            GameManager.instance.RestartGame();
        }
    }
    void Freeze()
    {
        GameManager.instance.RestartGame();
        //SoundManager.instance.FreezeClip();
    }
}
