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

        private Vector3 _scale;
        private Vector3 _prefabSize;
        
        private void Start()
        {
            _prefabSize = _linePrefab.ExtentsFilter.sharedMesh.bounds.size;
            _scale = _linePrefab.ExtentsFilter.transform.localScale;
        }

        public List<TimetableLine> Spawn(TimetableVariant variant)
        {
            List<TimetableLine> _lineObjects = new List<TimetableLine>();
            for (int i=0; i<variant.Lines.Count; i++) 
            {
                TimetableLine line = Instantiate(_linePrefab, _parent, false);
                line.SetText(variant.Lines[i]);
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
            return _firstSpawn.position - index * new Vector3(0, _prefabSize.y*_scale.y, 0);
        }
    }
}