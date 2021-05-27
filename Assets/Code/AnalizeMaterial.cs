using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnalizeMaterial : MonoBehaviour
{
    [SerializeField] private Text _text;
    private float _speed;

    public string Chemical
    {
        set { _text.text = value; }
        get { return _text.text; }
    }

    public float Speed 
    {
        set { _speed = value; }
    }

    public void FixedUpdate()
    {
        transform.localPosition += -Vector3.forward * _speed;
    }
}
