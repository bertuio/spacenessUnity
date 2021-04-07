using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private Animator _animator;
    /*[SerializeField] private float _blendingInertia;
    [SerializeField] private float _blendingMax;

    private float _walkBlend = 0;

    public void StartWalking() 
    {
        _blendingInertia = Mathf.Abs(_blendingInertia);
    }

    public void StopWalking() 
    {
        _blendingInertia = -Mathf.Abs(_blendingInertia);
    }

    private void FixedUpdate()
    {
        if (_blendingInertia < 0 && _walkBlend > 0) { _walkBlend += _blendingInertia; }
        if (_blendingInertia > 0 && _walkBlend < _blendingMax) { _walkBlend += _blendingInertia; }

        _animator.SetFloat("Speed", _walkBlend);
        Debug.Log(_walkBlend);
    }*/
}
