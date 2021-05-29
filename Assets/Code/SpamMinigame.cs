using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamMinigame : Minigame
{
    [SerializeField] private int _checkCounts;
    [SerializeField] private int _maxAlerts;
    [SerializeField] private int _medianDelay;

    [SerializeField] private SpamSpawner _spawner;
    protected override string MinigameName { get; set; } = "Compliance with_A8110J formalities";

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
        _spawner.Spawn(_checkCounts,_maxAlerts,_medianDelay);
    }

    public override void FinishGame()
    {
        base.FinishGame();
        _spawner.Flush();
    }

    public override void InterruptGame()
    {
        base.InterruptGame();
        _spawner.Flush();
    }
}
