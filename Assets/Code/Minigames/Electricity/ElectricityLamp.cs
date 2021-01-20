using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityLamp : MonoBehaviour
{
    public enum State 
    {
        Disabled,
        Enabled,
        Corrupted
    }

    [SerializeField] private bool _startEnabled;
    public bool Enabled => _state == State.Enabled;
    public bool Corrupted => _state == State.Corrupted;

    private State _state;
    [SerializeField] private Material _disabledMaterial, _enabledMaterial, _corruptedMaterial, _awaitingMaterial;
    [SerializeField] private GameObject _lightingMesh;
    private Vector2Int _cell = new Vector2Int(-1,-1);
    public Vector2Int Cell { get { return _cell; } set { if (_cell == new Vector2Int(-1, -1)) _cell = value; } }

    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = _lightingMesh.GetComponent<MeshRenderer>();
        if (_startEnabled) Enable();
    }

    public void Awating() 
    {
        _meshRenderer.material = _awaitingMaterial;
    }

    public void Corrupt() 
    {
        _state =  State.Corrupted;
        _meshRenderer.material = _corruptedMaterial;
    }

    public void Enable()
    {
        _state = State.Enabled;
        _meshRenderer.material = _enabledMaterial;
    }

    public void Disable()
    {
        _state = State.Disabled;
        _meshRenderer.material = _disabledMaterial;
    }
}
