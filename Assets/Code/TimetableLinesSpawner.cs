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
        private List<TimetableLine> _lineObjects = new List<TimetableLine>();

        public void Spawn(TimetableVariant variant) {
            
            Flush();
            Vector3 scale = _linePrefab.transform.localScale;
            Vector3 modifiedScale = new Vector3(scale.x, scale.y * ((float)_height / variant.Lines.Count), scale.z);
            for (int i=0; i<variant.Lines.Count; i++) 
            {
                TimetableLine line = Instantiate(_linePrefab,  _parent);
                line.transform.localScale = modifiedScale;
                line.transform.position = _firstSpawn.position-i*new Vector3(0, _linePrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size.y*modifiedScale.y, 0);
                line.Value.text = variant.Lines[i];
                _lineObjects.Add(line);
            }
        }

        public void Flush()
        {
            foreach (TimetableLine line in _lineObjects)
            {
                Destroy(line.gameObject);
            }
            _lineObjects.Clear();
        }
    }
}