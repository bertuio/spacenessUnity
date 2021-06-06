using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameCoffe : Minigame
{
    [SerializeField] private CoffeeMug _mugPrefab;
    [SerializeField] private Transform _mugSpawnPosition;
    [SerializeField] CoffeGrabber _grabber;

    private new void Awake() 
    {
        base.Awake();
        _grabber.WinCondition += FinishGame;
    }
    public override void StartGame()
    {
        _grabber.Activate();
        base.StartGame();
    }

    public override void FinishGame()
    {
        _grabber.Deactivate();
        Instantiate(_mugPrefab, _mugSpawnPosition);
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
