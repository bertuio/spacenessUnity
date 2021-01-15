using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float _doorSpeed;
    Action _moveUp;
    public void OpenDoor()
    {
        _moveUp += MoveUp;
    }

    private void MoveUp()
    {
        transform.position += transform.TransformVector(new Vector3(_doorSpeed,0,0));
    }

    private void Update()
    {
        _moveUp?.Invoke();
    }
}
