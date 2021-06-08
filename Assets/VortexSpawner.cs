using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexSpawner : MonoBehaviour
{
    [SerializeField] private Vortex _sample;

    public void Spawn() 
    {
        Instantiate(_sample, transform);
    }
}
