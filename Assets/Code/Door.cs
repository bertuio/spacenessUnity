using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private MeshRenderer _controlPanelIndicator;
    [SerializeField] private MeshRenderer _ceremonialTape;
    [SerializeField] private GameObject _ceremonialTapelText;

    [SerializeField] private Material _greenTape;
    [SerializeField] private Material _greenLamp;

    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _doorSpeed;
    Action _moveUp;
    public void OpenDoor()
    {
        Material[] a = _controlPanelIndicator.materials;
        a[1] = _greenLamp;
        _controlPanelIndicator.materials = a;
        
        if (_ceremonialTape)
            _ceremonialTape.material = _greenTape;
        _moveUp += MoveUp;
        if (_ceremonialTapelText)
            Destroy(_ceremonialTapelText);

        if (_audio)
            _audio.Play();
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
