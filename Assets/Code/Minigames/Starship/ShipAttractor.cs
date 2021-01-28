using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ShipAttractor : MonoBehaviour
{
    private Action _movementAffect;
    private Ship _ship;
    private SphereCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out _ship)) 
        {
            _movementAffect += AffectVelocity;
        }
    }

    private void AffectVelocity() 
    {
        _ship.AddPhysicsVelocity(ComputeAffectVelocity(transform.position - _ship.transform.position).normalized*0.03f);
    }

    private Vector2 ComputeAffectVelocity(Vector3 vector) 
    {
        vector = _ship.transform.InverseTransformVector(vector)*Mathf.Pow(Mathf.Clamp(1-vector.magnitude/_collider.radius,0,1),3);
        return new Vector2(vector.z, vector.x);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out _ship))
        {
            _movementAffect -= AffectVelocity;
            _ship = null;
        }
    }

    private void FixedUpdate()
    {
        _movementAffect?.Invoke();
    }
}
