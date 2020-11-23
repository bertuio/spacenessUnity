using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{

    [SerializeField]
    private float _linearSmoothRate;

    [SerializeField]
    private float _angularSmoothRate;

    private float defaultFov;
    private float _nodAngle;
    private Transform _parentalTransform;
    private Vector3 _cameraLinearOffset;
    private Quaternion _cameraAngularOffset;
    private Minigame _currentMinigame;
    private Action _cameraBehaviour;
    private Camera camera;
    private float targetFov;

    public void VortexAffect(float t)
    {
        targetFov = defaultFov * (Mathf.Clamp(1.8f-Mathf.Pow(t,2),1,2));
    }

    private void Start()
    {
        camera = GetComponent<Camera>();
        targetFov = defaultFov = camera.fieldOfView;
    }

    public float LinearSmoothRate
    {
        get { return _linearSmoothRate; }
        private set { }
    }
    public float AngularSmoothRate
    {
        get { return _angularSmoothRate; }
        private set { }
    }

    private void Update()
    {
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetFov, 0.02f);
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
