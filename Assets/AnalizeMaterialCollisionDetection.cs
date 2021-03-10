using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalizeMaterialCollisionDetection : MonoBehaviour
{
    public Action OnTriggerExited, OnTriggerEntered;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AnalizeMaterial material)) { OnTriggerEntered?.Invoke();}
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out AnalizeMaterial material)) { OnTriggerExited?.Invoke(); }
    }
}
