using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "qtEvent", menuName = "Data/qtEvent")]
public class qtEvent : ScriptableObject
{
    public Sprite Image => _image;
    public InputAction InputEvent => _event;

    [SerializeField] private Sprite _image;
    [SerializeField] private InputAction _event;
}