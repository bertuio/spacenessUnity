using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimetableLine : MonoBehaviour
{
    private Vector3 _targetPosition;
    private Action _moveAction;
    private float _movementSpeed;
    public Text Value;
    private int _index = -1;
    public int Index { get { return _index; } set { if (_index < 0) _index = value; } }

    private void Start()
    {
        _movementSpeed = 0.003f;
    }
    public void MoveTo(Vector3 position)
    {
        _targetPosition = position;
        _targetPosition.x = transform.position.x;
        _moveAction += MakeStep;
    }

    private void MakeStep()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _movementSpeed);
        if (transform.position == _targetPosition)
        {
            _moveAction -= MakeStep;
        }
    }

    private void Update()
    {
        _moveAction?.Invoke();
    }

}
