using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameButton : MonoBehaviour
{
    public enum ButtonDirection
    {
        UP, DOWN
    }
    [SerializeField] private ButtonDirection _direction;
    public ButtonDirection Direction { get { return _direction; } }
}
