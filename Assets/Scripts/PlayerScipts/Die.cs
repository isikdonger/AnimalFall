using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public static Animator animator;
    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = null;
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        animator.ResetTrigger("Freeze");
        animator.ResetTrigger("Unfreeze");
    }
    public void Froze()
    {
        GameManager.instance.Death();
        Destroy(GameObject.FindGameObjectWithTag("Player"));
    }
    public void Unfreeze()
    {
        if(animator.speed == -1)
        {
            animator.speed = 0;
            animator.SetTrigger("Unfreeze");
            animator.ResetTrigger("Freeze");
        }
    }
}
