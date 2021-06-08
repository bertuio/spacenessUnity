using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexSpawner : MonoBehaviour
{
    [SerializeField] private Vortex _sample;
    [SerializeField] private Transform _returnPointTransform;
    public void Spawn() 
    {
        Instantiate(_sample, transform)._returnTranform = _returnPointTransform;
    }
}
