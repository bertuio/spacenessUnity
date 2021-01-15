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
    private Action _currentMovementAction;
    public InputAction OnRightClick, OnLeftClick, OnMouseMoveX;
    private Action<InputAction.CallbackContext> _leftClickCallback, _rightClickCallback, _mouseMoveXCallback;

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
        _currentMovementAction?.Invoke();
    }
    private void InitializeInputActions() 
    {
        _leftClickCallback = (InputAction.CallbackContext context) =>
        {
            OnRightClick.Enable();
            OnLeftClick.Disable();
            RestartStepTimer();
            if (_currentMovementAction != Walk)
            {
                OnStartedWalking?.Invoke();
                SetAction(Walk);
            }
        };
        _rightClickCallback = (InputAction.CallbackContext context) =>
        {
            OnRightClick.Disable();
            OnLeftClick.Enable();
            RestartStepTimer();
            if (_currentMovementAction != Walk)
            {
                OnStartedWalking?.Invoke();
                SetAction(Walk);
            }
        };
        _mouseMoveXCallback = (InputAction.CallbackContext context) =>
        {
            AppendBodyRotation(context.ReadValue<float>());
        };
        
        OnLeftClick.performed += _leftClickCallback;
        OnRightClick.performed += _rightClickCallback;
        OnMouseMoveX.performed += _mouseMoveXCallback;
    }
    private void SetAction(Action movementAction)
    {
        _currentMovementAction = movementAction;
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

    public void Lock() 
    {
        OnLeftClick.Disable();
        OnRightClick.Disable();
        OnMouseMoveX.Disable();
    }

    public void Unlock() 
    {
        OnLeftClick.Enable();
        OnRightClick.Enable();
        OnMouseMoveX.Enable();
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
