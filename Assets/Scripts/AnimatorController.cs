using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public bool isRunning = false;
    public Animator animator;

    private bool isBlocking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeRunningAnimation()
    {
        isRunning = !isRunning;

        if (isRunning)
        {
            if (isBlocking)
                animator.SetTrigger("block_1");
            else
                animator.SetTrigger("run_cycle");
        }
        else
        {
            if (isBlocking)
                animator.SetTrigger("block_2");
            else
                animator.SetTrigger("idle");
        }
    }

    public void attackAnimation()
    {
        if (isRunning)
            animator.SetTrigger("attack_1");
        else
            animator.SetTrigger("attack_2");
    }
    public void enemyAttackAnimation()
    {
            animator.SetTrigger("attack");
    }

    public void blockAnimation()
    {
        isBlocking = true;
        if (isRunning)
            animator.SetTrigger("block_1");
        else
            animator.SetTrigger("block_2");
    }

    public void parryAnimation()
    {
        animator.SetTrigger("parry");
    }

    public void endAnimation()
    {
        isBlocking = false;
        if (isRunning)
            animator.SetTrigger("run_cycle");
        else
            animator.SetTrigger("idle");
    }
}
