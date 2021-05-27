using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour
{
    [SerializeField] private Image _timerImage;
    [SerializeField] private RectTransform _container;
    public void UpdateUI(float value) 
    {
        _timerImage.color = Color.Lerp(Color.red, Color.white, value);
        _timerImage.rectTransform.localScale = Vector3.Lerp(Vector3.up, Vector3.one, value);
    }

    public void Show()
    {
        _container.gameObject.SetActive(true);
    }
    public void Hide()
    {
        _container.gameObject.SetActive(false);
    }
}
