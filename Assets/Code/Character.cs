using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private class CharacterStateMachine
    {
        [SerializeField]
        private float _maxClickDelay;
        private float _lastClickTime;
        private bool _isWalking;

        private Action activeState;

        public CharacterStateMachine()
        {
            setActiveState(idle);
            _maxClickDelay = 0.85f;
            _lastClickTime = Time.time;
            _isWalking = false;
        }
        void idle()
        {
            _isWalking = false;
            if (Input.GetMouseButtonDown(0))
            {
                _lastClickTime = Time.time;
                setActiveState(walk_rightClick);
            }
        }
        void walk_leftClick()
        {
            _isWalking = true;
            if (Input.GetMouseButtonDown(0)) {
                _lastClickTime = Time.time;
                setActiveState(walk_rightClick);
            }
            if (Time.time - _lastClickTime > _maxClickDelay)
            {
                setActiveState(idle);
            }
        }
        void walk_rightClick()
        {
            _isWalking = true;
            if (Input.GetMouseButtonDown(1))
            {
                _lastClickTime = Time.time;
                setActiveState(walk_leftClick);
            }
            if (Time.time - _lastClickTime > _maxClickDelay)
            {
                setActiveState(idle);
            }

        }
        void setActiveState(Action state)
        {
            activeState = state;
        }
        public bool IsWalking 
        {
            get { return _isWalking; }
        }
        public void Update()
        {
            activeState();
        }
    }

    [SerializeField]
    private GameObject _mainCam;
    private Vector3 _cameraLinearOffset;
    private Quaternion _cameraAngularOffset;
    private CharacterController _characterController;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private float _linearSmoothRate;
    [SerializeField]
    private float _angularSmoothRate;
    private float _nodAngle;
    private CharacterStateMachine _CSM;
    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _cameraLinearOffset = transform.InverseTransformPoint(_mainCam.transform.position);
        _cameraAngularOffset = Quaternion.Inverse(transform.rotation)* _mainCam.transform.rotation;
        _characterController = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
        _CSM = new CharacterStateMachine();
    }

    void Update() 
    {
        ComputeCameraTransform();
        PerformCharacterMovement();
        _CSM.Update();
    }
    void PerformCharacterMovement()
    {
        _characterController.transform.rotation = Quaternion.Euler(new Vector3(0, _rotationSpeed, 0) * Input.GetAxis("Mouse X") * Time.deltaTime) * _characterController.transform.rotation;
        _anim.SetBool("Walking", _CSM.IsWalking);
        if (_CSM.IsWalking)
        {
            _characterController.Move(transform.forward * _speed * Time.deltaTime);
        }
    }

    void ComputeCameraTransform() 
    {
        Vector3 newCameraPosition = Vector3.Lerp(_mainCam.transform.position, transform.TransformPoint(_cameraLinearOffset), _linearSmoothRate);
        _mainCam.transform.position = newCameraPosition;
        _mainCam.transform.rotation = Quaternion.Lerp(_mainCam.transform.rotation, transform.rotation * _cameraAngularOffset, _angularSmoothRate);
        _nodAngle += Input.GetAxis("Mouse Y") * _rotationSpeed;
        //TODO: add nod rotation
    }
}