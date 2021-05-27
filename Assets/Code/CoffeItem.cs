using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoffeItem : MonoBehaviour
{
    private float _maxSpeed = 1;
    public Vector3 targetPosition;
    private void Awake()
    {
        targetPosition = transform.InverseTransformPoint(transform.position);
    }
    private void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.InverseTransformPoint(transform.position), transform.InverseTransformPoint(targetPosition), _maxSpeed);
    }
}
