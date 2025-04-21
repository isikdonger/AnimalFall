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
        animator.ResetTrigger("Idle");
    }
    public void Freeze()
    {
        GameManager.instance.Death();
        Destroy(GameObject.FindGameObjectWithTag("Player"));
    }
    public void Unfreeze()
    {
        animator.SetTrigger("Idle");
    }
}
