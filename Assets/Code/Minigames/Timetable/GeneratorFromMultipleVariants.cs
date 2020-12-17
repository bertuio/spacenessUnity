using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Timetable
{
    public class GeneratorFromMultipleVariants : TimetableGenerator
    {
        [SerializeField] private TimetableVariant[] _variants;
        public override TimetableVariant GetVariant()
        {
            return _variants[Random.Range(0, _variants.Length)];
        }
    }
}