using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnittingSpawner : MonoBehaviour
{
    [SerializeField] private KnittingButton _prefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private float _speed;
    [SerializeField] private float _spawnRadius;
    [SerializeField] private int _targetsCount;

    private float _speedMultiplyer = 1f;
    private float _sloweringCoefficient = .9f;
    private Action _onUpdate;
    public Action OnWinCondition;

    private Dictionary<KnittingButton, Vector3> _buttons = new Dictionary<KnittingButton, Vector3>();
    public void StartSpawning() 
    {
        for (int i = 0; i < _targetsCount; i++)
        {
            KnittingButton button = Instantiate(_prefab, _parent);
            Vector3 direction = (Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)) * new Vector3(1, 0, 0));
            button.transform.localPosition += direction * _spawnRadius * UnityEngine.Random.Range(0.5f, 1f);
            AddButton(button, -direction/1000*_speed);
        }
        _onUpdate += MoveButtons;
    }

    private void OnDisable()
    {
        _onUpdate -= MoveButtons;
    }

    private void MoveButtons()
    {
        foreach (KeyValuePair<KnittingButton, Vector3> pair in _buttons)
        {
            if (pair.Key.transform.localPosition.magnitude >= _spawnRadius)
            {

                pair.Key.transform.localPosition = new Vector3(0, 0, pair.Key.transform.localPosition.z);
            }
            pair.Key.transform.localPosition += pair.Value * _speedMultiplyer;
        }
    }

    private void SlowDown()
    {
        _speedMultiplyer *= _sloweringCoefficient;
    }

    public void AddButton(KnittingButton button, Vector3 direction)
    {
        button.OnClicked += SlowDown;
        button.OnClicked += delegate { _buttons.Remove(button); if (_buttons.Count == 0) OnWinCondition?.Invoke(); };
        _buttons.Add(button, direction);
    }

    private void FixedUpdate()
    {
        _onUpdate?.Invoke();
    }

    public void Flush()
    {
        _onUpdate -= MoveButtons;
        foreach (KeyValuePair<KnittingButton, Vector3> pair in _buttons) 
        {
            Destroy(pair.Key.gameObject);
        }
        _buttons.Clear();
        _speedMultiplyer = 1;
    }
}
