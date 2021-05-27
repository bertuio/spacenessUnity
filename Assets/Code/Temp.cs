using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    private float bias = 0;
    private Vector3 _startpos;
    void Start()
    {
        _startpos = transform.localPosition;
    }
    void FixedUpdate()
    {
        bias += -0.02f;
        transform.localPosition = _startpos + Vector3.right * bias;
        if (bias <= -7) { bias = 7; }
    }
}
