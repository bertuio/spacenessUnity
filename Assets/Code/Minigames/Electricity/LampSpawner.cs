using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSpawner : MonoBehaviour
{
    [SerializeField] private ElectricityLamp _lampPrefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private Transform _startTransform;
    [SerializeField] private Vector2Int _dimensions;
    [SerializeField] private float _gridSize;
    public float CorruptionProbability { get; set; }
    private float size_y = 0.6f;
    private float size_x = 1.1f;

    public ElectricityLamp[,] Spawn() 
    {
        float scale_x = size_x/(_gridSize*(_dimensions.x-1));
        float scale_y = size_y / (_gridSize * (_dimensions.y - 1));

        float scale = Mathf.Min(1,Mathf.Min(scale_x, scale_y));

        ElectricityLamp[,] _lamps = new ElectricityLamp[_dimensions.x, _dimensions.y];
        for (int i = 0; i < _dimensions.x; i++) 
        {
            for (int j = 0; j < _dimensions.y; j++)
            {
                _lamps[i, j] = Instantiate(_lampPrefab, _parent);
                _lamps[i, j].Cell = new Vector2Int(i,j);
                _lamps[i, j].transform.localPosition = _startTransform.localPosition + new Vector3(j, 0, i) * _gridSize * scale;
                _lamps[i, j].transform.localScale = new Vector3(scale, scale, scale);
                if (Random.Range(0f, 1f) <= CorruptionProbability) 
                {
                    _lamps[i, j].Corrupt();
                }
            }
        }
        _lamps[0, 0].Enable();
        _lamps[_dimensions.x-1, _dimensions.y-1].Corrupt();
        return _lamps;
    }
}
