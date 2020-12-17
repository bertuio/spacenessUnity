using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] PlayerInput _interactionPlayerInput;

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
        Debug.Log("Press registered");
        _currentInteractable?.Interact();
        //_interactionPlayerInput.DeactivateInput();
    }

    private void OnEnteredInteractable(Interactable interactable) 
    {
        _currentInteractable = interactable;
        _interactionPlayerInput.ActivateInput();
        interactable.PlayerEntered();
    }
    private void OnExitedInteractable(Interactable interactable)
    {
        _interactionPlayerInput.DeactivateInput();
        interactable.PlayerExited();
    }
}
