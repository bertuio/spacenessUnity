using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameButton : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public enum ButtonDirection
    {
        UP, DOWN
    }
    [SerializeField] private ButtonDirection _direction;
    public ButtonDirection Direction { get { return _direction; } }

    public void Press()
    {
        if (Direction == ButtonDirection.UP)
            _animator.SetTrigger("upperButton");
        else
            _animator.SetTrigger("lowerButton");
    }
}
