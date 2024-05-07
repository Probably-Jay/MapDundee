using UnityEngine;

namespace ExtraUtility
{
    public static class ScriptableExtensions
    {
        public static MonoBehaviour GetTempoaryMonobehaviour(this ScriptableObject scriptable) => new GameObject().AddComponent<TempoaryMonobehaviour>();
    }
}