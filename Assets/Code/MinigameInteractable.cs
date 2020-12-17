using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MinigameInteractable : Interactable
{
    [SerializeField] private UnityEvent _openGateway, _closeGateway;
    public Action OnInteracted;

    private void EnteredCallback() 
    {
        _openGateway?.Invoke();
    }

    private void ExitedCallback() 
    {
        _closeGateway?.Invoke();
        Camera.main.GetComponent<CameraController>().ForceChasing();
    }

    private void OnEnable()
    {
        _onEntered += EnteredCallback;
        _onExited += ExitedCallback;
    }

    private void OnDisable()
    {
        _onEntered -= EnteredCallback;
        _onExited -= ExitedCallback;
    }

    public override void Interact() 
    {
        _hint.Hide();
        OnInteracted?.Invoke();
        Debug.Log("Interacted");
        //_openGateway?.Invoke();
    }
}