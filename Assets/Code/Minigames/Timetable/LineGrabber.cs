using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LineGrabber : MonoBehaviour
{
    [SerializeField] private InputAction _onLeftMouseDown, _onLeftMouseUp, _onMouseMoveY;
    [SerializeField] private float _mouseLimit;
    public Action<TimetableLine> OnMoveUp, OnMoveDown;
    private Action<InputAction.CallbackContext> _leftMouseDownCallback, _leftMouseUpCallback;
    private TimetableLine _pickedLine;
    private float _mouseDelta;
    private void OnDisable()
    {
        Deactivate();
    }

    private void Start()
    {
        InitializeInputActions();
    }
    public void Activate()
    {
        _onLeftMouseDown.Enable();
        _onLeftMouseUp.Enable();
        _onMouseMoveY.Disable();
    }
    public void Deactivate()
    {
        _onLeftMouseDown.Disable();
        _onLeftMouseUp.Disable();
        _onMouseMoveY.Disable();
    }

    private void InitializeInputActions()
    {
        _leftMouseDownCallback = (InputAction.CallbackContext context) =>
        {
            TryGrab();
        };
        _leftMouseUpCallback = (InputAction.CallbackContext context) =>
        {
            StopGrabbing();
        };
        _onLeftMouseDown.performed += _leftMouseDownCallback;
        _onLeftMouseUp.performed += _leftMouseUpCallback;
    }
    private TimetableLine TraceLine() 
    {
        RaycastHit hit;
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        TimetableLine line;

        if (Physics.Raycast(ray, out hit) && hit.collider.TryGetComponent(out line))
        {
            return line;
        }
        else
        {
            return null;
        }
    }

    private void _mouseMoveYCallback(InputAction.CallbackContext context) 
    {
        _mouseDelta += context.ReadValue<float>();
        if (Mathf.Abs(_mouseDelta) > _mouseLimit)
        {
            if (_mouseDelta < 0)
            {
                OnMoveDown?.Invoke(_pickedLine);
            }
            else
            {
                OnMoveUp?.Invoke(_pickedLine);
            }
            _mouseDelta = 0;
        }
    }

    private void TryGrab() 
    {
        _pickedLine = TraceLine();
        if (!_pickedLine) return;
        _onMouseMoveY.performed += _mouseMoveYCallback;

        _onMouseMoveY.Enable();
    }

    private void StopGrabbing() 
    {
        _onMouseMoveY.Disable(); 
        _onMouseMoveY.performed -= _mouseMoveYCallback;
        _mouseDelta = 0;
    }
}