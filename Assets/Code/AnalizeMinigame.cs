using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalizeMinigame : Minigame
{
    [SerializeField] private AnalizeMaterialSpawner _spawner;
    [SerializeField] private MaterialPicker _picker;
    [SerializeField] private AnalizeMaterial _display;
    [SerializeField] private AudioSource _audioMissed;
    private string _targetChemical;
    private new void Awake()
    {
        base.Awake();
        _picker.OnMaterialPicked += CompareMaterials;
    }

    public override void StartGame()
    {
        base.StartGame();
        LoadGame();
    }

    private void LoadGame()
    {
        _spawner.Clear();
        _targetChemical = _spawner.RandomElement();
        _display.Chemical = _targetChemical;
        _spawner.Launch();
        _picker.Activate();
    }

    public override void FinishGame()
    {
        base.FinishGame();
        _picker.Deactivate();
        _spawner.Clear();
    }

    public override void InterruptGame()
    {
        base.InterruptGame();
        _picker.Deactivate();
        _spawner.Clear();
    }
    private void CompareMaterials(AnalizeMaterial material)
    {
        if (material.Chemical == _targetChemical)
        {
            FinishGame();
        }
        else 
        {
            LoadGame();
            _audioMissed.Play();
        }
    }
}
