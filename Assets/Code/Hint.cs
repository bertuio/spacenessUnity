using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hint : MonoBehaviour
{
    [SerializeField] protected Canvas _dravingCanvas;
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
    public virtual void Display() { }
    public abstract void Hide();
}