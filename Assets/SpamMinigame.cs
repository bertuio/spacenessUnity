using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamMinigame : Minigame
{
    [SerializeField] private int _checkCounts;
    [SerializeField] private int _maxAlerts;
    [SerializeField] private int _medianDelay;

    [SerializeField] private SpamSpawner _spawner;
    public override void StartGame()
    {
        base.StartGame();
        _spawner.Spawn(_checkCounts,_maxAlerts,_medianDelay);
    }
}
