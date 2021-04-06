using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InteractionHint : Hint
{
    [SerializeField] private Animator _hintAnimator;
    private void Start()
    {
        Hide();
    }
    public override void Display()
    {
        _hintAnimator.SetBool("hide", false);
    }

    public override void Hide()
    {
        _hintAnimator.SetBool("hide", true);
    }
}
