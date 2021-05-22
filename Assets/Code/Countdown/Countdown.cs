using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    private static float _speed = 1;
    private static bool _isPaused = false;
    public static void Pause() 
    {
        _isPaused = true;
        UpdateSpeed();
    }
    public static void Unpause()
    {
        _isPaused = false;
        UpdateSpeed();
    }

    public static void SpeedDown()
    {
        _speed -= .2f;
        UpdateSpeed();
    }
    public static void SpeedUp()
    {
        _speed += .2f;
        UpdateSpeed();
    }

    private static void UpdateSpeed() 
    {
        CountdownUI.SpeedMultiplyer = _speed * (_isPaused?0:1);
    }
}
