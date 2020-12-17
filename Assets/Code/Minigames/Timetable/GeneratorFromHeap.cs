using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Timetable
{
    public class GeneratorFromHeap : TimetableGenerator
    {
        [SerializeField] private TimetableVariant _heap;
        [SerializeField] private int _requiredNumberOfLines;
        public override TimetableVariant GetVariant()
        {
            List<string> newLines = new List<string>(_heap.Lines);
            int linesToDelete = newLines.Count - _requiredNumberOfLines;
            for (int i = 0; i < linesToDelete; i++)
            {
                newLines.RemoveAt(Random.Range(0, newLines.Count));
            }

            return new TimetableVariant(newLines);
        }
    }
}