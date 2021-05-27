using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ResearchTurnDeadzone : MonoBehaviour
{
    public Action onCharacterEntered;
    private SphereCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character)) 
        {
            onCharacterEntered?.Invoke();
        }
    }
}
