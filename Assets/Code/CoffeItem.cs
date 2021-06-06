using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoffeItem : MonoBehaviour
{
    [SerializeField] private bool _needed;
    public bool IsCoffee => _needed;
    public void UpdatePosition(Vector3 position)
    {
        Debug.Log(position);
        transform.position = position;
    }
}
