using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private Vector2 _constantVelocity = Vector2.up;
    private Vector2 _velocity, _physicsVelocity;
    public Action OnShipCrushed, OnShipSuccsesed;
    private Action _movementAction;
    private float _sideSpeed = 1.5f;
    [SerializeField] private float _speed;

    private void Awake()
    {
        _velocity = _constantVelocity*_speed;
        _movementAction += Move;
        _physicsVelocity = Vector2.zero;
    }
    void FixedUpdate()
    {
        _movementAction?.Invoke();
    }

    public void AddPhysicsVelocity(Vector2 vector) 
    {
        _physicsVelocity += vector;
    }

    private void Move()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-Vector3.up * Vector3.SignedAngle(new Vector3(-(_velocity - _physicsVelocity).y, 0, (_velocity + _physicsVelocity).x), -Vector3.right, Vector3.up)) * transform.rotation, 0.01f);
        Debug.DrawLine(transform.position, transform.position + transform.TransformVector(new Vector3((_physicsVelocity).y, 0, (_physicsVelocity).x)*5));
        transform.position += transform.TransformVector(new Vector3(-(_velocity).y, 0, 0));
        _physicsVelocity = Vector2.zero;
    }

    public void SetManualVelocity(Vector2 velocity) 
    {
        _velocity = (_constantVelocity*velocity.y + new Vector2(velocity.x, 0)*_sideSpeed) * _speed;
    }

    public void Crush()
    {
        Debug.Log("Ship crushed");
        _movementAction -= Move;
        OnShipCrushed?.Invoke();
    }

    public void Succses() 
    {
        Debug.Log("Ship succsessed");
        _movementAction -= Move;
        OnShipSuccsesed?.Invoke();
    }
}
