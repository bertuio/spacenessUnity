using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    private static float _speed = 1;
    public static void SpeedDown()
    {
        _speed -= .2f;
        CountdownUI.SpeedMultiplyer = _speed;
    }
    public static void SpeedUp()
    {
        _speed += .2f;
        CountdownUI.SpeedMultiplyer = _speed;
    }
}
