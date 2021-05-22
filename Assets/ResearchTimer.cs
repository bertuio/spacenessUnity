using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchTimer : MonoBehaviour
{
    [SerializeField] private TimerDisplay _display;
    public float timerMax = 8;
    private float _timer;
    public bool isPlaying = true;
    public event System.Action OnTimeExceeded;

    private void Awake()
    {
        _timer = timerMax;
    }
    private void UpdateTimer() 
    {
        if (isPlaying) 
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0) { OnTimeExceeded?.Invoke(); }
        }
        _display.UpdateUI(_timer / timerMax);
    }

    public void Rewind() 
    {
        _timer = timerMax;
    }

    private void Update()
    {
        UpdateTimer();
    }

    public void Show()
    {
        Rewind();
        isPlaying = true;
        _display.Show();
    }
    public void Hide()
    {
        isPlaying = false;
        _display.Hide();
    }
}
