using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnittingDecor : MonoBehaviour
{
    [SerializeField] private float _speed;
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, _speed, 0));
    }
}
