using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    [SerializeField] private Ship _ship;
    public Action OnShipCrushed, OnShipSuccsesed;

    private void Awake()
    {
        _ship.OnShipCrushed += () => { OnShipCrushed(); };
        _ship.OnShipSuccsesed += () => { OnShipSuccsesed(); };
    }

    public void SetSideVelocity(Vector2 velocity) 
    {
        velocity = MapShipAcceleration(velocity);
        _ship.SetManualVelocity(velocity);
    }

    private Vector2 MapShipAcceleration(Vector2 a) 
    {
        a.y = a.y / 4 + 1;
        return a;
    }
}
