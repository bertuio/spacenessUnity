using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalizeMaterialSpawner : MonoBehaviour
{
    [SerializeField] private AnalizeMaterial _materialPrefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private string[] _elements;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private AnalizeMaterialCollisionDetection _start, _end;
    [SerializeField] private float _conveyorSpeed;
    private List<AnalizeMaterial> _instances = new List<AnalizeMaterial>();

    public void Launch() 
    {
        Debug.Log("Launched");
        SpawnChemical();
    }

    public string RandomElement() 
    {
        return _elements[Random.Range(0, _elements.Length)];
    }

    public void Clear() 
    {
        _start.OnTriggerEntered -= SpawnChemical;
        _instances.ForEach((AnalizeMaterial material) => { Destroy(material.gameObject); });
        _instances.Clear();
        _start.OnTriggerEntered += SpawnChemical;
    }

    private void SpawnChemical()
    {
        AnalizeMaterial spawnedMaterial = Instantiate(_materialPrefab, _spawnTransform.position, _spawnTransform.rotation, _parent);
        spawnedMaterial.Speed = _conveyorSpeed;
        spawnedMaterial.Chemical = RandomElement();
        _instances.Add(spawnedMaterial);
    }

    private void OnEnable()
    {
        _start.OnTriggerEntered += SpawnChemical;
        _end.OnTriggerEntered += OnEndArrivedCallback;
    }

    private void OnDisable()
    {
        _start.OnTriggerEntered -= SpawnChemical;
        _end.OnTriggerEntered -= OnEndArrivedCallback;
    }
    private void OnEndArrivedCallback()
    {
        Destroy(_instances[0].gameObject);
        _instances.RemoveAt(0);
    }
}
