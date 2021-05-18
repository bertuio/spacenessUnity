using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LampGrabber : MonoBehaviour
{
    [SerializeField] private InputAction _onLeftMouseDown, _onLeftMouseUp, _onMouseMove;
    private Action<InputAction.CallbackContext> _leftMouseDownCallback, _leftMouseUpCallback;
    private ElectricityLamp _pickedLamp, _linkedLamp;
    [SerializeField] private WireDrawer _drawer;
    public Action OnLampEnable;
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
        _onMouseMove.Disable();
    }
    public void Deactivate()
    {
        _onLeftMouseDown.Disable();
        _onLeftMouseUp.Disable();
        _onMouseMove.Disable();
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
    private ElectricityLamp TraceLamp()
    {
        RaycastHit hit;
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        ElectricityLamp lamp;

        if (Physics.Raycast(ray, out hit) && hit.collider.TryGetComponent(out lamp))
        {
            return lamp;
        }
        else
        {
            return null;
        }
    }

    private void MouseMoveCallback(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent(out _linkedLamp))
            {
                _drawer.ReplaceEnd(_linkedLamp.transform);
            }
            else
            {
                _linkedLamp = null;
                _drawer.ReplaceEnd(hit.point, _pickedLamp.transform);
            }
        }
    }

    public void FlushWires() 
    {
        _drawer.Flush();
    }

    private void TryGrab()
    {
        _pickedLamp = TraceLamp();
        if (!_pickedLamp || !_pickedLamp.Enabled)
        {
            _pickedLamp = null;
            return;
        }

        _onMouseMove.performed += MouseMoveCallback;
        
        _onMouseMove.Enable();

        _drawer.SetupNewLineRenderer(_pickedLamp.transform);
    }

    private void StopGrabbing()
    {
        _onMouseMove.Disable();
        _onMouseMove.performed -= MouseMoveCallback;
        if (_pickedLamp) {
            if (_linkedLamp && !_linkedLamp.Enabled && CheckNeighbourhood(_pickedLamp.Cell, _linkedLamp.Cell))
            {
                _linkedLamp.Enable();
                OnLampEnable?.Invoke();
            }
            else
            {
                _drawer.DestroyLast();
            } 
        }
        _linkedLamp = null;
        _pickedLamp = null;
    }

    private bool CheckNeighbourhood(Vector2Int a, Vector2Int b) 
    {
        return a + Vector2Int.up == b || a + Vector2Int.right == b || a + Vector2Int.left == b || a + Vector2Int.down == b;
    }
}