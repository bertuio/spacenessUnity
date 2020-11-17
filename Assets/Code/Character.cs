using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private class CharacterStateMachine
    {
        private Character _character;
        private float _maxClickDelay;
        private float _lastClickTime;
        public bool IsWalking { get; private set; }
        public bool IsLocked { get; private set; }

        private Action activeState;

        public CharacterStateMachine(Character character)
        {
            setActiveState(idle);
            _maxClickDelay = 0.85f;
            _lastClickTime = Time.time;
            IsWalking = false;
            _character = character;
        }
        private void idle()
        {
            IsWalking = false;
            if (Input.GetMouseButtonDown(0))
            {
                _lastClickTime = Time.time;
                setActiveState(walk_rightClick);
            }
        }
        private void walk() 
        {
            IsWalking = true;
            if (Time.time - _lastClickTime > _maxClickDelay / _character._speedMultyplier)
            {
                setActiveState(idle);
            }
        }
        private void walk_leftClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _lastClickTime = Time.time;
                setActiveState(walk_rightClick);
            }
            walk();
        }
        private void walk_rightClick()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _lastClickTime = Time.time;
                setActiveState(walk_leftClick);
            }
            walk();
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
    [Range(0,100)]
    private int _health;
    private float _speedMultyplier = 1;
    [SerializeField]
    private CameraController _attachedCameraController;
    private CharacterController _characterController;
    [SerializeField]
    private float _baseSpeed;
    [SerializeField]
    private float _rotationSpeed;
    private CharacterStateMachine _CSM;
    private Animator _anim;
    public CameraController AttachedCameraController { get => _attachedCameraController; private set { }}

    private void Start()
    {
        _attachedCameraController.SetCameraChasingGoal(transform);
        _characterController = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
        _CSM = new CharacterStateMachine(this);
    }

    private void Update() 
    {
        PerformCharacterMovement();
        SetupSpeed();
        _CSM.Update();
    }

    private void SetupSpeed()
    {
        _speedMultyplier = (float)(_health*0.75f+25) / 100;
        _anim.speed = _speedMultyplier;
    }

    private void PerformCharacterMovement()
    {
        if (!_CSM.IsLocked)
        {
            _characterController.transform.rotation = Quaternion.Euler(new Vector3(0, _rotationSpeed*_speedMultyplier, 0) * Input.GetAxis("Mouse X") * Time.deltaTime) * _characterController.transform.rotation;
        }
        _anim.SetBool("Walking", _CSM.IsWalking);
        if (_CSM.IsWalking)
        {
            _characterController.Move(transform.forward * _baseSpeed * _speedMultyplier * Time.deltaTime);
        }
    }

    public void SetMovementLock(bool locked) 
    {
        _CSM.SetLock(locked);
    }
}