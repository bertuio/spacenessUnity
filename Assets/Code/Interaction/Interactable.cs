using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public abstract class Interactable : MonoBehaviour
{   
    protected Action _onEntered;
    protected Action _onExited;
    public Action OnInteracted, OnInteractionEnded;
    public Action OnInteractionEndedForced;
    private void Awake()
    {

    }

    public abstract void EndInteraction();
    public abstract void StartInteraction();

    public void PlayerEntered()
    {
        _onEntered?.Invoke();
    }

    public void PlayerExited()
    {
        _onExited?.Invoke();
    }
}