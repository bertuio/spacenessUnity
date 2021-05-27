using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SpamItem : MonoBehaviour
{
    public bool Toggled { get; private set; }
    public Action activated;
    [SerializeField] private Sprite _checkedSprite;

    public void Toggle()
    {
        Toggled = true;
        Debug.Log("toggled");
        Button _button = GetComponent<Button>();
        _button.interactable = false;
        _button.image.sprite = _checkedSprite;
        activated?.Invoke();
    }
}
