using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    
    private class CountdownTime 
    {
        int days, hours, minutes;
        float seconds;

        public CountdownTime(int days, int hours, int minutes, float seconds)
        {
            this.days = days;
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
        }

        public void Subtract(int days, int hours, int minutes, float seconds) 
        {
            this.seconds -= seconds;
            this.minutes -= minutes;
            this.hours -= hours;
            this.days -= days;

            if (this.seconds < 0)
            {
                this.seconds += 60;
                this.minutes -= 1;
            }
            if (this.minutes < 0)
            {
                this.minutes += 60;
                this.hours -= 1;
            }
            if (this.hours < 0)
            {
                this.hours += 24;
                this.days -= 1;
            }
        }

        public override string ToString() 
        {
            return $"{days}:{hours}:{minutes}:{(int)seconds}";
        }
    }

    [SerializeField] private CountdownUI ui;
    private CountdownTime countdownTime = new CountdownTime(7,0,0,0);
    private Action OnUpdate;
    private void Start()
    {
        DontDestroyOnLoad(this);
        Resume();
    }
    public void Resume()
    {
        OnUpdate = delegate {
            countdownTime.Subtract(0, 0, 0, Time.deltaTime);
            ui.counterValue = countdownTime.ToString();
        };
    }

    public void Pause()
    {
        OnUpdate = null;
    }

    public void Update()
    {
        OnUpdate?.Invoke();
    }
}
