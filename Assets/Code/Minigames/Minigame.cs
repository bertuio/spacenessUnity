using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Minigame : MonoBehaviour
{
    [SerializeField] protected MinigameCamera _minigameCamera;
    [SerializeField] protected MinigameInteractable _interactable;

    public abstract void StartGame();

    private void Awake()
    {
        _interactable.OnInteracted += Interact;
    }

    public void Interact()
    {
        StartGame();
    }
}
