using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialContainer : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private int _showTrigger, _hideTrigger;

    private void Awake()
    {
        _showTrigger = Animator.StringToHash("Show");
        _hideTrigger = Animator.StringToHash("Hide");
    }

    public void Show() 
    {
        _animator.SetTrigger(_showTrigger);
    }

    public void Hide()
    {
        _animator.SetTrigger(_hideTrigger);
    }
}
