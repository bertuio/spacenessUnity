using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Minigame : MonoBehaviour
{
    [SerializeField] protected MinigameCamera _minigameCamera;
    [SerializeField] protected MinigameInteractable _interactable;

    public abstract void StartGame();
    public abstract void EndGame();

    private void Awake()
    {
        _interactable.OnInteracted += Interact;
        _interactable.OnFinishInteraction += FinishInteraction;
    }

    public void Interact()
    {
        StartGame();
    }

    public void FinishInteraction() 
    {
        EndGame();
    }
}
