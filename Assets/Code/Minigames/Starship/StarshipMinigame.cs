using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarshipMinigame : Minigame
{
    [SerializeField] private ShipControl _control;
    [SerializeField] private Space _spacePrefab;
    private Space _spaceInstance;
    protected override string MinigameName { get; set; } = "W_xAB7291D_arp engine";

    public override void StartGame()
    {
        base.StartGame();
        _spaceInstance = Instantiate(_spacePrefab, new Vector3(100,100,100), Quaternion.identity, transform);
        _control.Activate();

        _control.VelocityChangedCallback += _spaceInstance.SetSideVelocity;
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
        if (_spaceInstance)
        {
            _control.VelocityChangedCallback -= _spaceInstance.SetSideVelocity;
            _spaceInstance.OnShipCrushed -= FailGame;
            _spaceInstance.OnShipSuccsesed -= FinishGame;
        }
    }
}
