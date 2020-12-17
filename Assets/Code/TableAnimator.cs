using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableAnimator : MonoBehaviour
{
    public Animator animator;

    public void Close() 
    {
        animator.SetBool("Opened", false);
    }

    public void Open() 
    {
        animator.SetBool("Opened", true);
    }
}
