using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Minigame : MonoBehaviour
{
    [SerializeField] protected MinigameCamera _minigameCamera;
    [SerializeField] protected MinigameInteractable _interactable;
    [SerializeField] private UnityEvent _onMinigameFinished;

    public virtual void StartGame() 
    {
        Camera.main.gameObject.GetComponent<CameraController>().SimulateCamera(_minigameCamera);
    }
    public virtual void InterruptGame() 
    {
        Camera.main.GetComponent<CameraController>().ForceChasing();
    }
    public virtual void FinishGame()
    {
        Camera.main.GetComponent<CameraController>().ForceChasing();

        Debug.Log("Before callback");
        _onMinigameFinished?.Invoke();
        Debug.Log("After callback");
        _interactable.OnInteractionEndedForced?.Invoke();
    }

    private void Awake()
    {
        _interactable.OnInteracted += StartGame;
        _interactable.OnInteractionEnded += InterruptGame;
    }
}
