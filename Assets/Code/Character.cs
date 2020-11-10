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
        public bool IsWalking { get; private set; }
        public bool IsLocked { get; private set; }

        private Action activeState;

        public CharacterStateMachine()
        {
            setActiveState(idle);
            _maxClickDelay = 0.85f;
            _lastClickTime = Time.time;
            IsWalking = false;
        }
        void idle()
        {
            IsWalking = false;
            if (Input.GetMouseButtonDown(0))
            {
                _lastClickTime = Time.time;
                setActiveState(walk_rightClick);
            }
        }
        void walk_leftClick()
        {
            IsWalking = true;
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
            IsWalking = true;
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
        private void locked() 
        {
            IsWalking = false;
        }
        public void SetLock(bool isLocked) 
        {
            if (isLocked)
            {
                setActiveState(locked);
            }
            else { setActiveState(idle); }
            IsLocked = isLocked;
        }

        void setActiveState(Action state)
        {
            activeState = state;
        }
        public void Update()
        {
            activeState();
        }
    }

    [SerializeField]
    private CameraController _attachedCameraController;
    private CharacterController _characterController;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotationSpeed;
    private CharacterStateMachine _CSM;
    private Animator _anim;
    [SerializeField]
    private GameObject _head;

    public GameObject Head { get => _head; private set { } }
    public CameraController AttachedCameraController { get => _attachedCameraController; private set { }}

    private void Start()
    {
        _attachedCameraController.SetCameraChasingGoal(transform);
        _characterController = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
        _CSM = new CharacterStateMachine();
    }

    private void Update() 
    {
        PerformCharacterMovement();
        _CSM.Update();
    }
    private void PerformCharacterMovement()
    {
        if (!_CSM.IsLocked)
        {
            _characterController.transform.rotation = Quaternion.Euler(new Vector3(0, _rotationSpeed, 0) * Input.GetAxis("Mouse X") * Time.deltaTime) * _characterController.transform.rotation;
        }
        _anim.SetBool("Walking", _CSM.IsWalking);
        if (_CSM.IsWalking)
        {
            _characterController.Move(transform.forward * _speed * Time.deltaTime);
        }
    }

    public void SetMovementLock(bool locked) 
    {
        _CSM.SetLock(locked);
    }
}