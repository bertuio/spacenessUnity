using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityMinigame : Minigame
{
    [SerializeField] private LampSpawner _spawner;
    [SerializeField] private LampGrabber _grabber;
    [SerializeField] private float _corruptionProbability;
    [SerializeField] private float _recorruptionProbability;

    private ElectricityLamp[,] _lamps;

    private void Start()
    {
        _spawner.CorruptionProbability = _corruptionProbability;
    }

    private void OnEnable()
    {
        _grabber.OnLampEnable += OnLampEnableCallback;
    }
    private void OnDisable()
    {
        _grabber.OnLampEnable -= OnLampEnableCallback;
    }

    private void OnLampEnableCallback() 
    {
        Recorrupt();
        CheckWinCondition();
    }
    private void Recorrupt() 
    {
        if (Random.Range(0.000f, 1.000f)<=_recorruptionProbability) 
        {
            _lamps[Random.Range(0, _lamps.GetLength(0)), Random.Range(0, _lamps.GetLength(1))].Corrupt();
        }
    }
    private void CheckWinCondition() 
    {
        foreach (ElectricityLamp lamp in _lamps) 
        {
            if (lamp.Corrupted) 
            {
                return;
            }
        }
        FinishGame();
    }

    public override void StartGame()
    {
        base.StartGame();
        _lamps = _spawner.Spawn();
        _grabber.Activate();
    }

    public override void InterruptGame()
    {
        base.InterruptGame();
        Flush();
        _grabber.Deactivate();
    }

    public override void FinishGame()
    {
        base.FinishGame();
        Flush();
        _grabber.Deactivate();
    }

    private void Flush() {
        foreach (ElectricityLamp i in _lamps) 
        {
            Destroy(i.gameObject);
        }
        _lamps = new ElectricityLamp[0, 0];
        _grabber.FlushWires();
    }
}
