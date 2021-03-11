using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralResearchBoulderSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private MineralResearchBoulder _boulderPrefab;

    private void Awake()
    {
       Instantiate(_boulderPrefab, _spawnPoints[Random.Range(0, _spawnPoints.Length)]);
    }
}
