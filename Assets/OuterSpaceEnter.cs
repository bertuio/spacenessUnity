using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterSpaceEnter : Interactable
{
    public Action<Character> OnEnteted;
    public override void StartInteraction()
    {
        OnEnteted?.Invoke(FindObjectOfType<Character>());
    }

    public override void EndInteraction()
    {

    }
}
