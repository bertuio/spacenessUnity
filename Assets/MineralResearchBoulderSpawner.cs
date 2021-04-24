using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralResearchBoulderSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private MineralResearchBoulder _boulderPrefab;
    [SerializeField] private Vector3 _center;
    [SerializeField] private float _internalRadius, _externalRadius, _height, _startAngle, _endAngle, _bottomBorder, _topBorder;
    [SerializeField] private int _steps, _heightCells;

    private void Awake()
    {
       Instantiate(_boulderPrefab, _spawnPoints[Random.Range(0, _spawnPoints.Length)]);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_center, 4);
        
        Gizmos.color = Color.red;

        for (int i = 0; i < _steps; i++)
        {
            float angle = Mathf.Lerp(_startAngle, _endAngle, (float)i / (_steps-1));

            for (int j = 0; j < _heightCells; j++)
            {
                float height = Mathf.Lerp(_bottomBorder, _topBorder, (float)j / (_heightCells - 1));
                Gizmos.DrawLine(_center + Vector3.up*height + Quaternion.Euler(0, angle, 0) * Vector3.right * _internalRadius, _center  + Vector3.up * height + Quaternion.Euler(0, angle, 0) * Vector3.right * _externalRadius);
            }
        }
    }


}
