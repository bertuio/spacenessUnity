using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarshipMinigame : Minigame
{
    [SerializeField] private ShipControl _control;
    [SerializeField] private Space _spacePrefab;
    private Space _spaceInstance;

    public override void StartGame()
    {
        base.StartGame();
        _spaceInstance = Instantiate(_spacePrefab, new Vector3(100,100,100), Quaternion.identity, transform);
        _control.Activate();
        _control.VelocityChagedCallback += _spaceInstance.SetSideVelocity;
        _spaceInstance.OnShipCrushed += FailGame;
        _spaceInstance.OnShipSuccsesed += FinishGame;
    }

    public override void FinishGame()
    {
        base.FinishGame();
        Debug.Log("Game finished");
        UnsubControlEvents();
        if (_spaceInstance) Destroy(_spaceInstance.gameObject);
    }

    public override void InterruptGame()
    {
        base.InterruptGame();
        UnsubControlEvents();
        if (_spaceInstance) Destroy(_spaceInstance.gameObject);
    }

    public override void FailGame()
    {
        base.FailGame();
        Debug.Log("Game failed");
        UnsubControlEvents();
        if (_spaceInstance) Destroy(_spaceInstance.gameObject);
    }

    private void UnsubControlEvents()
    {
        _control.Deactivate();
        _control.VelocityChagedCallback -= _spaceInstance.SetSideVelocity;
        _spaceInstance.OnShipCrushed -= FailGame;
        _spaceInstance.OnShipSuccsesed -= FinishGame;
    }
}
