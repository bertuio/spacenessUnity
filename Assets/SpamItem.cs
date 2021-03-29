using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SpamItem : MonoBehaviour
{
    private bool _toggled = false;
    public Action activated;
    [SerializeField] private Sprite _checkedSprite;

    public void Toggle()
    {
        Debug.Log("toggled");
        Button _button = GetComponent<Button>();
        _button.interactable = false;
        _button.image.sprite = _checkedSprite;
        activated?.Invoke();
    }
}
