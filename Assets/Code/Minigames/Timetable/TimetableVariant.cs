using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Timetable
{
    [CreateAssetMenu(fileName = "TimetableVariant", menuName = "Data/TimetableVariant")]
    public class TimetableVariant : ScriptableObject
    {
        [SerializeField] private List<string> _lines;

        public List<string> Lines => _lines;

        public TimetableVariant(List<string> lines)
        {
            _lines = lines;
        }
    }

}