using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{

    [SerializeField]
    private float _linearSmoothRate;

    [SerializeField]
    private float _angularSmoothRate;

    private float _nodAngle;
    private Transform _parentalTransform;
    private Vector3 _cameraLinearOffset;
    private Quaternion _cameraAngularOffset;
    private Minigame _currentMinigame;
    private Action _cameraBehaviour;

    private void Update()
    {
        _cameraBehaviour?.Invoke();
    }

    public void SetCameraChasingGoal(Transform parentalTransform)
    {
        _parentalTransform = parentalTransform;
        _cameraLinearOffset = parentalTransform.InverseTransformPoint(transform.position);
        _cameraAngularOffset = Quaternion.Inverse(parentalTransform.rotation) * transform.rotation;
        ForceChasing();
    }

    public void ForceChasing() 
    {
        _cameraBehaviour = Chase;
    }

    public void SetCameraMinigame(Minigame minigame) 
    {
        _currentMinigame = minigame;
        _cameraBehaviour = minigame.CameraBehaviour;
    }

    private void Chase() 
    {
        if (_parentalTransform)
        {
            Vector3 newCameraPosition = Vector3.Lerp(transform.position, _parentalTransform.TransformPoint(_cameraLinearOffset), _linearSmoothRate);
            transform.position = newCameraPosition;
            transform.rotation = Quaternion.Lerp(transform.rotation, _parentalTransform.rotation * _cameraAngularOffset, _angularSmoothRate);
        }
    }
}
