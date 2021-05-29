using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerAnimations Animations => _animation;
    [SerializeField] private float _stepTime;
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _flySpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _rotationInertion;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private PlayerAnimations _animation;

    private Quaternion _targetRotation;
    private float _stepTimer;
    private Action _currentMovementAction;
    public InputAction OnRightClick, OnLeftClick, OnMouseMoveX;
    private Action<InputAction.CallbackContext> _leftClickCallback, _rightClickCallback, _mouseMoveXCallback;
    private Vector3 _flightTargetPosition;

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
                _animation.StartWalking();
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
                _animation.StartWalking();
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
        transform.rotation = Quaternion.Lerp(_controller.transform.rotation, _targetRotation, _rotationInertion);
    }
    private bool UpdateStepTimer() 
    {
        _stepTimer -= Time.deltaTime;
        return _stepTimer <= 0;
    }

    public void LockMovement() 
    {
        _controller.enabled = false;
        OnLeftClick.Disable();
        OnRightClick.Disable();
        _animation.StopWalking();
        SetAction(Idle);
    }

    public void LockMovementAndRotation()
    {
        _controller.enabled = false;
        OnLeftClick.Disable();
        OnRightClick.Disable();
        OnMouseMoveX.Disable();
        _animation.StopWalking();
        SetAction(Idle);
    }

    public void Unlock()
    {
        _controller.enabled = true;
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
        _controller.Move(_animation.SpeedMeasure * transform.forward * _baseSpeed * Time.deltaTime);
    }

    private void Walk() 
    {
        ApplyBodyRotation();
        _controller.Move(_animation.SpeedMeasure * transform.forward * _baseSpeed * Time.deltaTime);
        if (UpdateStepTimer())
        {
            _animation.StopWalking();
            SetAction(Idle);
        }
    }

    public void StopFly()
    {
        _animation.PlayFly();
        _animation.StopWalking();
        SetAction(Idle);
    }

    public void FlyTowards(Vector3 position)
    {
        if (_currentMovementAction != Fly)
        {
            _animation.PlayFly();
        }
        _flightTargetPosition = position;
        SetAction(Fly);
    }

    private void Fly() 
    {
        transform.position = Vector3.MoveTowards(transform.position, _flightTargetPosition, _flySpeed*Time.deltaTime);
        ApplyBodyRotation();
    }
}
