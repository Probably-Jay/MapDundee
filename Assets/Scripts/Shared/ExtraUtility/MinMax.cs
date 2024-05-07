using System;
using UnityEngine;

namespace ExtraUtility
{
    [Serializable]
    public struct MinMax
    {
        [field: SerializeField] public float Min { get; set; }
        [field: SerializeField] public float Max { get; set; }

        public readonly float Clamp(float value) => Mathf.Clamp(value, Min, Max);
    }
}
