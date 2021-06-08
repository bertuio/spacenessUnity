using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public PlayerAnimations Animations => _movement.Animations;
    [SerializeField] private CameraController _attachedCameraController;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerInteraction _interaction;

    private static Character _character;
    public static Character GetCharacter() 
    {
        return _character;
    }
    public CameraController AttachedCameraController { get => _attachedCameraController; private set { }}

    private void Start()
    {
        _character = this;
    }

    private void OnEnable()
    {
        _attachedCameraController.SetCameraChasingGoal(transform);

        Debug.Log("Teleportation to return point!!!");
        if (SceneManager.GetActiveScene().buildIndex == Vortex.SceneIdOfReturn)
        {
            transform.position = Vortex.NextReturnPosition;
        }
    }

    public void FlyTo(Vector3 position)
    {
        _movement.FlyTowards(position);
    }
    public void StopFly()
    {
        _movement.StopFly();
    }

    public void LockMovement()
    {
        _movement.LockMovement();
    }

    public void LockMovementAndRotation() 
    {
        _movement.LockMovementAndRotation();
    }

    public void UnlockMovement() 
    {
        _movement.Unlock();
    }
}