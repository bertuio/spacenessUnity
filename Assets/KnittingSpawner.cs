using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnittingSpawner : MonoBehaviour
{
    [SerializeField] private KnittingButton _prefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _spawnRadius;
    [SerializeField] private float _failCoefficient, _winCoefficient;
    [SerializeField] private float _sloweringCoefficient;
    public event Action OnButtonClicked, OnButtonFailed;

    private Vector3 _direction;
    private KnittingButton _button;
    private float _speedMultiplyer = 1f;
    private Action _onUpdate;
    public Action OnWinCondition, OnFailCondition;

    public void StartSpawning() 
    {
        SpawnButton();
        _onUpdate += MoveButtons;
    }

    private void SpawnButton() 
    {
        if (_button) Destroy(_button.gameObject);
        _button = Instantiate(_prefab, _parent);
        _button.OnClicked += ButtonClickedCallback;
        _direction = (Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)) * new Vector3(1, 0, 0));
        _button.transform.localPosition += _direction * _spawnRadius * UnityEngine.Random.Range(0.8f, 1.2f);
    }

    private void OnDisable()
    {
        _onUpdate -= MoveButtons;
    }

    private void MoveButtons()
    {
        if (_button.transform.localPosition.magnitude <=  .04f)
        {
            OnButtonFailed?.Invoke();
            _speedMultiplyer /= _sloweringCoefficient;
            SpawnButton();
            if (_speedMultiplyer >= _failCoefficient) OnFailCondition?.Invoke();
        }
        _button.transform.localPosition += -_direction/1000*_baseSpeed*_speedMultiplyer;
    }

    private void ButtonClickedCallback()
    {
        OnButtonClicked?.Invoke();
        _speedMultiplyer *= _sloweringCoefficient;
        Destroy(_button.gameObject);
        if (_speedMultiplyer <= _winCoefficient) { OnWinCondition?.Invoke(); return; }
        SpawnButton();
    }

    private void FixedUpdate()
    {
        _onUpdate?.Invoke();
    }

    public void Flush()
    {
        _onUpdate -= MoveButtons;
        if (_button) Destroy(_button.gameObject);
        _speedMultiplyer = 1;
    }
}
