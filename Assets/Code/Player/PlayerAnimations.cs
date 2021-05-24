using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public float SpeedMeasure => _walkBlend/_blendingMax;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private AudioSource _audioStep;
    [SerializeField] private Animator _animator;
    //---
    [SerializeField] private float _blendingInertia;
    [SerializeField] private float _blendingMax;
    private float _walkBlend = 0;

    [SerializeField] private float _reverseInertia;
    [SerializeField] private float _reverseMax;
    private float _reverseBlend = 0;

    public void StartReverse()
    {
        _reverseInertia = Mathf.Abs(_reverseInertia);
    }

    public void StopReverse()
    {
        _reverseInertia = -Mathf.Abs(_reverseInertia);
    }


    public void step()
    {
        _audioStep.Play();
    }

    public void PlayFly()
    {
        _animator.SetTrigger("SpaceTrigger");
    }
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
        if (_blendingInertia < 0 && _walkBlend > 0) { _walkBlend = Mathf.Clamp(_walkBlend + _blendingInertia, 0, _blendingMax); }
        if (_blendingInertia > 0 && _walkBlend < _blendingMax) { _walkBlend = Mathf.Clamp(_walkBlend + _blendingInertia, 0, _blendingMax); }

        _animator.SetFloat("Speed", _walkBlend);

        if (_reverseInertia < 0 && _reverseBlend > 0) { _reverseBlend = Mathf.Clamp(_reverseBlend + _reverseInertia, 0, _reverseMax); }
        if (_reverseInertia > 0 && _reverseBlend < _reverseMax) { _reverseBlend = Mathf.Clamp(_reverseBlend + _reverseInertia, 0, _reverseMax); }

        _animator.SetFloat("Reverse", _reverseBlend);
    }
}
