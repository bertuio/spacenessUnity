using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameCoffe : Minigame
{
    [SerializeField] CoffeGrabber _grabber;

    public override void StartGame()
    {
        _grabber.Activate();
        base.StartGame();
    }

    public override void FinishGame()
    {
        _grabber.Deactivate();
        base.FinishGame();
    }

    public override void FailGame()
    {
        _grabber.Deactivate();
        base.FailGame();
    }

    public override void InterruptGame()
    {
        _grabber.Deactivate();
        base.InterruptGame();
    }
}
