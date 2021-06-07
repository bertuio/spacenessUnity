using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _doorSpeed;
    Action _moveUp;
    public void OpenDoor()
    {
        _moveUp += MoveUp;
        if (_audio) _audio.Play();
    }

    private void MoveUp()
    {
        transform.position += transform.TransformVector(new Vector3(0, 0, -_doorSpeed));
    }

    private void FixedUpdate()
    {
        _moveUp?.Invoke();
    }
}
