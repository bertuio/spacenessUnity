using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Timetable {

    public class TimetableLinesSpawner : MonoBehaviour
    {
        [SerializeField] private int _height;
        [SerializeField] private TimetableLine _linePrefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _firstSpawn;
        private Vector3 _modifiedScale;
        private Vector3 _scale;
        private Vector3 _prefabSize;
        
        private void Start()
        {
            _prefabSize = _linePrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size;
        }

        public List<TimetableLine> Spawn(TimetableVariant variant)
        {
            _scale = _linePrefab.transform.localScale;
            _modifiedScale = new Vector3(_scale.x/variant.Lines.Count, _scale.y * Mathf.Min((float)_height / variant.Lines.Count, 1.0f), _scale.z);
            List<TimetableLine> _lineObjects = new List<TimetableLine>();
            for (int i=0; i<variant.Lines.Count; i++) 
            {
                TimetableLine line = Instantiate(_linePrefab,  _parent);
                line.transform.localScale = _modifiedScale;
                line.Value.text = variant.Lines[i];
                line.Index = i;
                _lineObjects.Add(line);
            }
            Shuffle(ref _lineObjects);
            for (int i = 0; i < variant.Lines.Count; i++)
            {
                _lineObjects[i].transform.position = GetPositionByIndex(i);
            }

            return _lineObjects;
        }
        private void Shuffle(ref List<TimetableLine> list)
        {
            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n);
                TimetableLine value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public Vector3 GetPositionByIndex(int index) 
        {
            return _firstSpawn.position - index * new Vector3(_modifiedScale.x * _prefabSize.x, _prefabSize.y * _modifiedScale.y, 0);
        }
    }
}