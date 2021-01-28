using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipControl : MonoBehaviour
{
    [SerializeField] private InputAction _onVerticalMovement, _onHorizontalMovement;
    public Action<Vector2> VelocityChagedCallback;
    private Vector2 _axes = Vector2.zero;
    private void Awake()
    {
        InitializeInputActions();
    }
    public void Activate()
    {
        _onVerticalMovement.Enable();
        _onHorizontalMovement.Enable();
    }
    public void Deactivate()
    {
        _onVerticalMovement.Disable();
        _onHorizontalMovement.Disable();
    }
    private void InitializeInputActions() 
    {
        _onVerticalMovement.performed += VerticalAxisCallback;
        _onHorizontalMovement.performed += HorizontalAxisCallback;
    }

    private void VerticalAxisCallback(InputAction.CallbackContext context)
    {
        _axes.y = context.ReadValue<float>();
        VelocityChagedCallback?.Invoke(_axes);
    }
    private void HorizontalAxisCallback(InputAction.CallbackContext context)
    {
        _axes.x = context.ReadValue<float>();
        VelocityChagedCallback?.Invoke(_axes);
    }
}
