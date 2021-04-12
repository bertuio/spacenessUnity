using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnittingMinigame : Minigame
{
    [SerializeField] private KnittingGrabber _grabber;
    [SerializeField] private KnittingSpawner _spawner;

    private void OnEnable() 
    {
        _spawner.OnWinCondition += FinishGame;
    }
    private void OnDisable()
    {
        _spawner.OnWinCondition -= FinishGame;
    }
    
    public override void StartGame()
    {
        base.StartGame();
        _spawner.StartSpawning();
        _grabber.Activate();
    }

    public override void FailGame()
    {
        base.FailGame();
        _grabber.Deactivate();
        _spawner.Flush();
    }

    public override void InterruptGame()
    {
        base.InterruptGame();
        _grabber.Deactivate();
        _spawner.Flush();
    }

    public override void FinishGame()
    {
        base.FinishGame();
        _grabber.Deactivate();
        _spawner.Flush();
    }
}
