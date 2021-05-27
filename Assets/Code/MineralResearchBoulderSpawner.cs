using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class MineralResearchBoulderSpawner : MonoBehaviour
{
    [SerializeField] private MineralResearchBoulder[] _boulderPrefabs;
    [SerializeField] private Vector3 _center;
    [SerializeField] private float _internalRadius, _externalRadius, _startAngle, _endAngle, _bottomBorder, _topBorder, _baseRadius;
    [SerializeField] private float _randomizePosition, _randomizeRadius;
    [SerializeField] private int _radialSteps, _heightSteps, _widthSteps;
    private Point[,,] _points;

    struct Point
    {
        public Vector3 position;
        public float radius;
    }

    private void OnValidate()
    {
        GeneratePoints();
    }

    private void Start()
    {
        if (!Application.isPlaying)
            return;

        int randI = Random.Range(0, _radialSteps);
        int randJ = Random.Range(0, _heightSteps);
        int randK = Random.Range(0, _widthSteps);

        for (int i = 0; i < _radialSteps; i++)
        {
            for (int j = 0; j < _heightSteps; j++)
            {
                for (int k = 0; k < _widthSteps; k++)
                {
                    MineralResearchBoulder boulder = Instantiate(_boulderPrefabs[Random.Range(0, _boulderPrefabs.Length)], transform, true);
                    boulder.transform.position = _points[k,j,i].position;
                    boulder.transform.localScale = Vector3.one*_points[k,j,i].radius;
                    if (randI == i & randJ == j & randK == k) boulder.MakeRare();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_center, 4);
        
        Gizmos.color = Color.white;

        for (int i = 0; i < _radialSteps; i++)
        {
            for (int j = 0; j < _heightSteps; j++)
            {
                for (int k = 0; k < _widthSteps; k++) {
                    Gizmos.DrawSphere(_points[k,j,i].position, _points[k, j, i].radius);
                }
            }
        }
    }

    private void GeneratePoints() 
    {
        _points = new Point[_widthSteps, _heightSteps, _radialSteps];

        for (int i = 0; i < _radialSteps; i++)
        {
            for (int j = 0; j < _heightSteps; j++)
            {
                for (int k = 0; k < _widthSteps; k++)
                {
                    _points[k, j, i].position = GetPosition(k, j, i);
                    _points[k, j, i].radius = _baseRadius * Mathf.Pow(Random.Range(1 - _randomizeRadius, 1 + _randomizeRadius), 3);
                }
            }
        }
    }
    private Vector3 GetPosition(int x, int y, int z) 
    {
        float angle = Mathf.Lerp(_startAngle, _endAngle, (_radialSteps==1)?.5f:(float) z / (_radialSteps - 1));
        float height = Mathf.Lerp(_bottomBorder, _topBorder, (_heightSteps == 1) ? .5f : (float)y / (_heightSteps - 1));
        float radius = Mathf.Lerp(_internalRadius, _externalRadius, (_widthSteps == 1) ? .5f : (float)x / (_widthSteps - 1));
        Vector3 randomize = new Vector3(Random.Range(-1f, 1), Random.Range(-1f, 1), Random.Range(-1f, 1)) * _randomizePosition;
        return _center + Vector3.up * height + Quaternion.Euler(0, angle, 0) * Vector3.right * radius + randomize;
    }

}
