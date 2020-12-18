using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInput _interactionPlayerInput;
    [SerializeField] private Hint _hint;

    public Action InteractionStarted, InteractionEnded;

    private Interactable _currentInteractable;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            OnEnteredInteractable(interactable);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            OnExitedInteractable(interactable);
        }
    }

    public void StartInteraction()
    {
        _hint.Hide();
        Debug.Log("Press registered");
        _currentInteractable?.Interact();
        InteractionStarted?.Invoke();
    }

    public void EndInteraction() 
    {
        _currentInteractable.FinishInteraction();
        InteractionEnded?.Invoke();
        _currentInteractable.PlayerExited();
    }

    private void OnEnteredInteractable(Interactable interactable) 
    {
        _hint.Display();
        _currentInteractable = interactable;
        _interactionPlayerInput.SwitchCurrentActionMap("Interactions");
        interactable.PlayerEntered();
    }
    private void OnExitedInteractable(Interactable interactable)
    {
        _hint.Hide();
        _interactionPlayerInput.SwitchCurrentActionMap("No interactions");
        interactable.PlayerExited();
        EndInteraction();
    }
}
