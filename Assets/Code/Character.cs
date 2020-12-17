using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private CameraController _attachedCameraController;
    private CharacterController _characterController;
    public CameraController AttachedCameraController { get => _attachedCameraController; private set { }}

    private void OnEnable()
    {
        _attachedCameraController.SetCameraChasingGoal(transform);
    }
}