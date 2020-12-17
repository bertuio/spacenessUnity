using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Timetable
{
    public abstract class TimetableGenerator:MonoBehaviour
    {
        public abstract TimetableVariant GetVariant();
    }
}