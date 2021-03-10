using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInput _interactionPlayerInput;
    [SerializeField] private Hint _hint;
    [SerializeField] private AudioSource _audioInteracted;

    public Action OnInteractionStarted, OnInteractionEnded, OnInteractionEndedForced;

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

    public void OnEnterButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _interactionPlayerInput.SwitchCurrentActionMap("InInteraction");
            StartInteraction();
        }
    }

    public void OnExitButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _interactionPlayerInput.SwitchCurrentActionMap("Ready");
            EndInteraction();
        }
    }
    private void StartInteraction()
    {
        _hint.Hide();
        Debug.Log("Press registered");
        _currentInteractable?.StartInteraction();
        OnInteractionStarted?.Invoke();
        if (_currentInteractable != null) _audioInteracted.Play();
    }

    private void EndInteraction() 
    {
        _hint.Hide();
        _currentInteractable?.EndInteraction();
        OnInteractionEnded?.Invoke();
    }

    private void InteractionEdedForcedCallback() 
    {
        _interactionPlayerInput.SwitchCurrentActionMap("Ready");
    }

    private void OnEnteredInteractable(Interactable interactable) 
    {
        _hint.Display();
        _currentInteractable = interactable;
        _interactionPlayerInput.SwitchCurrentActionMap("Ready");
        interactable.PlayerEntered();
        interactable.OnInteractionEndedForced += OnInteractionEndedForced;
        interactable.OnInteractionEndedForced += InteractionEdedForcedCallback;
        Debug.Log("ENTERED");
    }
    private void OnExitedInteractable(Interactable interactable)
    {
        _hint.Hide();
        _interactionPlayerInput.SwitchCurrentActionMap("NotReady");
        interactable.PlayerExited();
        interactable.OnInteractionEndedForced -= OnInteractionEndedForced;
        interactable.OnInteractionEndedForced -= InteractionEdedForcedCallback;
        EndInteraction();
    }
}