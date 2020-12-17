using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _stepTime;
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _rotationInertion;
    [SerializeField] private CharacterController _controller;

    private Quaternion _targetRotation;
    private float _stepTimer;
    private Action _movementAction;
    public InputAction OnRightClick, OnLeftClick, OnMouseMoveX;
    private Action<InputAction.CallbackContext> LeftClickCallback, RightClickCallback, MouseMoveXCallback;

    public Action OnStartedWalking, OnStartedIdle;
    private void OnEnable()
    {
        InitializeInputActions();
        SetAction(Idle);
        _targetRotation = transform.rotation;

        OnRightClick.Enable();
        OnLeftClick.Enable();
        OnMouseMoveX.Enable();
    }
    private void OnDisable()
    {
        OnRightClick.Disable();
        OnLeftClick.Disable();
        OnMouseMoveX.Disable();
    }
    private void Update()
    {
        _movementAction?.Invoke();
    }
    private void InitializeInputActions() 
    {
        LeftClickCallback = (InputAction.CallbackContext context) =>
        {
            OnRightClick.Enable();
            OnLeftClick.Disable();
            RestartStepTimer();
            if (_movementAction != Walk)
            {
                OnStartedWalking?.Invoke();
                SetAction(Walk);
            }
        };
        RightClickCallback = (InputAction.CallbackContext context) =>
        {
            OnRightClick.Disable();
            OnLeftClick.Enable();
            RestartStepTimer();
            if (_movementAction != Walk)
            {
                OnStartedWalking?.Invoke();
                SetAction(Walk);
            }
        };
        MouseMoveXCallback = (InputAction.CallbackContext context) =>
        {
            AppendBodyRotation(context.ReadValue<float>());
        };
        
        OnLeftClick.performed += LeftClickCallback;
        OnRightClick.performed += RightClickCallback;
        OnMouseMoveX.performed += MouseMoveXCallback;
    }
    private void SetAction(Action movementAction)
    {
        _movementAction = movementAction;
    }
    private void AppendBodyRotation(float rotation) 
    {
        _targetRotation = Quaternion.Euler(0, rotation*_rotationSpeed, 0) * _targetRotation;
    }
    private void ApplyBodyRotation() 
    {
        _controller.transform.rotation = Quaternion.Lerp(_controller.transform.rotation, _targetRotation, _rotationInertion);
    }
    private bool UpdateStepTimer() 
    {
        _stepTimer -= Time.deltaTime;
        return _stepTimer <= 0;
    }

    private void RestartStepTimer() 
    {
        _stepTimer = _stepTime;
    }
    private void Idle() 
    {
        ApplyBodyRotation();
    }

    private void Walk() 
    {
        ApplyBodyRotation();
        _controller.Move(transform.forward * _baseSpeed * Time.deltaTime);
        if (UpdateStepTimer())
        {
            OnStartedIdle?.Invoke();
            SetAction(Idle);
        }
    }
}
