using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCamera : MonoBehaviour
{
    [SerializeField] private Ship _ship;
    private Vector3 _offset;

    private void Awake()
    {
        _offset = transform.position - _ship.transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _ship.transform.position + _offset, 0.02f);
    }
}
