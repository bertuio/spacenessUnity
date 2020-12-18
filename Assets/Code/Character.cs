using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CameraController _attachedCameraController;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerInteraction _interaction;
    public CameraController AttachedCameraController { get => _attachedCameraController; private set { }}

    private void OnEnable()
    {
        _interaction.InteractionStarted += () => _movement.Lock();
        _interaction.InteractionEnded += () => _movement.Unlock();
        _attachedCameraController.SetCameraChasingGoal(transform);
    }
}