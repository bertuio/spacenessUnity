using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class CountdownUI : MonoBehaviour
{
    [SerializeField] private int _offset = 700;
    private float _posY;
    private Vector2 _position;
    public static float SpeedMultiplyer = 1f;

    private static TMP_Text _text;
    private static int _speed = 2;
    private int _canHeight;
    private void Start()
    {
        if (!_text)
        {
            _text = GetComponent<TMP_Text>();
        }
        _canHeight = (int) GetComponentInParent<Canvas>().pixelRect.height;
    }

    private void FixedUpdate()
    {
        _position = _text.rectTransform.offsetMax;
        _position.y = (-_offset + _posY);
        Debug.Log(_speed * SpeedMultiplyer);
        _posY = (_posY + _speed*SpeedMultiplyer) % (_canHeight + (_offset* 1.5f));
        _text.rectTransform.offsetMax = _position;
    }

    private static int _value;
    public static int Value {
        get {
            return _value; 
        }
        set { 
            _value = value;
            _text.text = $"Осталось \n{_value}\n дней";
        } 
    }
}
