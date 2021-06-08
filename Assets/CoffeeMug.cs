using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMug : Interactable
{
    public override void EndInteraction()
    {
    }

    public override void StartInteraction()
    {
        Countdown.SpeedUp();
        Countdown.SpeedUp();
        Countdown.SpeedUp();
        Countdown.SpeedUp();
        Countdown.SpeedUp();

        for (int i = 0; i < 5; i++) 
        {
            EventLogDisplay.display.AddEvent("YOU FEEL MUCH BETTER");
        }

        Destroy(gameObject);
    }
}
