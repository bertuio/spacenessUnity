using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CoffeGrabber : MonoBehaviour
{
    [SerializeField] private InputAction _onLeftMouseDown, _onLeftMouseUp, _onMouseMove;
    [SerializeField] private Transform _planeDirection;
    private event System.Action<InputAction.CallbackContext> _leftMouseDownCallback, _leftMouseUpCallback;
    private CoffeItem _pickedItem;
    private Vector3 _targetPositon;

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
    private CoffeItem TraceLamp()
    {
        RaycastHit hit;
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        CoffeItem lamp;

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
        Plane plane = new Plane(_planeDirection.up, _pickedItem.transform.position);
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y));

        if (plane.Raycast(ray, out float point))
        {
            _targetPositon = ray.GetPoint(point);
            _pickedItem.targetPosition = _targetPositon;
        }
            
    }

    private void StopGrabbing() 
    {
        _onMouseMove.performed -= MouseMoveCallback;
        _onMouseMove.Disable();
    }
    private void TryGrab()
    {
        _pickedItem = TraceLamp();

        if (_pickedItem)
        {
            _onMouseMove.performed += MouseMoveCallback;
            _onMouseMove.Enable();
        }
    }
}
