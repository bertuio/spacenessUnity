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

    private void Awake()
    {

    }

    public abstract void FinishInteraction();
    public abstract void Interact();

    public void PlayerEntered()
    {
        _onEntered?.Invoke();
    }

    public void PlayerExited()
    {
        _onExited?.Invoke();
    }
}