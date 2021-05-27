using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TMP_Text))]
public class CountdownUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float _posY;
    private int _canvasHeight;
    private Vector2 _position;

    private Color _mainColor = Color.white;
    public static float SpeedMultiplyer = 1f;
    private static TMP_Text _text;
    private static int _speed = 2;
    private static int _value;
    public static int Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            _text.text = $"{_value}\n ЦИКЛОВ ОСТАЛОСЬ";
        }
    }
    private void Awake()
    {
        if (!_text)
        {
            _text = GetComponent<TMP_Text>();
        }
        _canvasHeight = (int) GetComponentInParent<Canvas>().pixelRect.height;
    }

    private void FixedUpdate()
    {
        float height = _text.rectTransform.rect.height;
        _position = _text.rectTransform.anchoredPosition;
        _position.y = (-_canvasHeight + _posY);
        _posY = (_posY + _speed*SpeedMultiplyer) % (_canvasHeight + height);

        _text.rectTransform.anchoredPosition = _position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("A");
        _mainColor = _text.color;
        _text.color = Color.white;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _text.color = _mainColor;
    }
}
