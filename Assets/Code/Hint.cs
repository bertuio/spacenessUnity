using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Hint : MonoBehaviour
{
    [SerializeField] protected Image _drawings;
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
    public virtual void Display() { }
    public abstract void Hide();
}