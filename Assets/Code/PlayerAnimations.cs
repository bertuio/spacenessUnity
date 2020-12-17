using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private Animator _animator;
    private Action _startWalking;
    private Action _stopWalking;

    private void OnEnable()
    {
        InitializeMotionEvents();
        _movement.OnStartedWalking += _startWalking;
        _movement.OnStartedIdle += _stopWalking;
    }
    private void InitializeMotionEvents()
    {
        _startWalking = () => { _animator.SetBool("Walking", true); };
        _stopWalking = () => { _animator.SetBool("Walking", false); };
    }

    private void OnDisable()
    {
        _movement.OnStartedWalking -= _startWalking;
        _movement.OnStartedIdle -= _stopWalking;
    }
}
