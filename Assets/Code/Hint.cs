using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Hint : MonoBehaviour
{
    public virtual void Display() { }
    public abstract void Hide();
}