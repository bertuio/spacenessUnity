using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityMinigame : Minigame
{
    [SerializeField] private AudioClip[] _clicks;
    [SerializeField] private LampSpawner _spawner;
    [SerializeField] private LampGrabber _grabber;
    [SerializeField] private float _corruptionProbability;
    [SerializeField] private float _recorruptionProbability;

    private ElectricityLamp[,] _lamps = new ElectricityLamp[0, 0];

    private List<ElectricityLamp> GetDisabledLamps()
    {
        List<ElectricityLamp> list = new List<ElectricityLamp>();
        foreach (ElectricityLamp a in _lamps)
        {
            if (!a.Enabled && !a.Corrupted) list.Add(a);
        }
        return list;
    }

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
        TryRecorrupt();
        EmitSound(_clicks[Random.Range(0, _clicks.Length)]);
        CheckWinCondition();
    }
    private void TryRecorrupt() 
    {
        if (Random.Range(0.000f, 1.000f)<=_recorruptionProbability) 
        {
            List<ElectricityLamp> list = GetDisabledLamps();
            if (list.Count>0) list[Random.Range(0, list.Count)].Corrupt();
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
        Flush();
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
        _grabber.Deactivate();
        WonIllumination();
    }

    private void Flush() {

        foreach (ElectricityLamp i in _lamps) 
        {
            Destroy(i.gameObject);
        }
        _lamps = new ElectricityLamp[0, 0];
        _grabber.FlushWires();
    }

    private void WonIllumination() 
    {
        foreach (ElectricityLamp lamp in _lamps)
        {
            lamp.Awating();
        }
    }
}
