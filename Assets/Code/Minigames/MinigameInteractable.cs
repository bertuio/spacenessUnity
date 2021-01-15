using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MinigameInteractable : Interactable
{
    [SerializeField] private UnityEvent _openGateway, _closeGateway;

    private void EnteredCallback() 
    {
        _openGateway?.Invoke();
    }

    private void ExitedCallback() 
    {
        _closeGateway?.Invoke();
    }

    private void OnEnable()
    {
        _onEntered += EnteredCallback;
        _onExited += ExitedCallback;
        OnInteractionEndedForced += _closeGateway.Invoke;
    }

    private void OnDisable()
    {
        _onEntered -= EnteredCallback;
        _onExited -= ExitedCallback;
        OnInteractionEndedForced -= _closeGateway.Invoke;
    }

    

    public override void EndInteraction()
    {
        OnInteractionEnded?.Invoke();
        Debug.Log("Interaction Finished");
    }
    public override void StartInteraction() 
    {
        OnInteracted?.Invoke();
        Debug.Log("Interacted");
        //_openGateway?.Invoke();
    }
}