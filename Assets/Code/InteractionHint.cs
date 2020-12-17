﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InteractionHint : Hint
{
    private void Start()
    {
        Hide();
    }
    public override void Display()
    {
        Debug.Log("Displaying");
        _dravingCanvas.gameObject.SetActive(true);
    }

    public override void Hide()
    {
        _dravingCanvas.gameObject.SetActive(false);
    }
}
