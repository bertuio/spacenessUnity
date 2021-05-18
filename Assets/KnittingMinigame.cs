using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnittingMinigame : Minigame
{
    [SerializeField] private KnittingGrabber _grabber;
    [SerializeField] private KnittingSpawner _spawner;
    [SerializeField] private AudioClip[] _clicks;
    [SerializeField] private AudioClip[] _fails;

    private void OnDisable()
    {
        _spawner.OnWinCondition -= FinishGame;
        _spawner.OnFailCondition -= FailGame;
        _spawner.OnButtonClicked -= ButtonClickedHandler;
        _spawner.OnButtonFailed -= ButtonFailedHandler;
    }

    private void OnEnable()
    {
        _spawner.OnWinCondition += FinishGame;
        _spawner.OnFailCondition += FailGame;
        _spawner.OnButtonClicked += ButtonClickedHandler;
        _spawner.OnButtonFailed += ButtonFailedHandler;
    }

    private void ButtonClickedHandler()
    {
        EmitSound(_clicks[Random.Range(0, _clicks.Length)]);
    }
    private void ButtonFailedHandler()
    {
        EmitSound(_fails[Random.Range(0, _fails.Length)]);
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
