using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnittingGrabber : MonoBehaviour
{
    [SerializeField] private InputAction _onLeftMouseDown;
    [SerializeField] private AudioSource _hitAudio;
    private Action<InputAction.CallbackContext> _leftMouseDownCallback;

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
                if (hit.collider.TryGetComponent(out KnittingButton button))
                {
                    button.Click();
                    _hitAudio.Play();
                }
            }
        }
        catch (Exception) { }
    }

    private void TryGrab()
    {
        TraceInteractive();
    }
}