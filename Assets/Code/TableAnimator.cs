using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableAnimator : MonoBehaviour
{
    public Animator animator;

    public void Close()
    {
        animator.Play("Scene", 0, Mathf.Clamp01(animator.GetCurrentAnimatorStateInfo(0).normalizedTime));
        animator.SetFloat("Speed", 1.0f);
    }

    public void Open()
    {
        animator.Play("Scene", 0, Mathf.Clamp01(animator.GetCurrentAnimatorStateInfo(0).normalizedTime));
        animator.SetFloat("Speed", -1.0f);
    }
}
