using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LineGrabber : MonoBehaviour
{
    [SerializeField] private InputAction _onLeftMouseDown;
    [SerializeField] private float _mouseLimit;
    public Func<TimetableLine,TimetableLine> TryMoveUp, TryMoveDown;
    private Action<InputAction.CallbackContext> _leftMouseDownCallback;
    private TimetableLine _pickedLine;

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
    }
    public void Deactivate()
    {
        _onLeftMouseDown.Disable();
    }

    private void InitializeInputActions()
    {
        _leftMouseDownCallback = (InputAction.CallbackContext context) =>
        {
            TryGrab();
        };
        _onLeftMouseDown.performed += _leftMouseDownCallback;
    }
    private void TraceInteractive()
    {
        try
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out TimetableLine line))
                {
                    if (_pickedLine) _pickedLine.Dislight();
                    _pickedLine = line;
                    _pickedLine.Enlight();
                }
                else if (_pickedLine && hit.collider.TryGetComponent(out MinigameButton button))
                {
                    _pickedLine.Dislight();
                    if (button.Direction == MinigameButton.ButtonDirection.UP) _pickedLine = TryMoveUp.Invoke(_pickedLine);
                    else _pickedLine = TryMoveDown.Invoke(_pickedLine);
                    _pickedLine.Enlight();
                }
            }
        }
        catch (Exception){ }
    }

    private void TryGrab() 
    {
        TraceInteractive();
    }
}