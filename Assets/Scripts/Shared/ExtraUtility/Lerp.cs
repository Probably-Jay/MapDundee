using System;
using UnityEngine;

namespace ExtraUtility
{
    public static class Lerp
    {
        // Freya Holmer method for frame-rate independant decaying lerp https://x.com/FreyaHolmer/status/1757918211679650262

        public static float SmoothLerp(float a, float b, float deltaTime, float timeToTarget, float precision = 0.01f) 
            => Mathf.Lerp(a, b, GetLerpValue(deltaTime, timeToTarget, precision));  
        
        public static float SmoothLerp(float a, float b, float deltaTime, float halfLifeTime) 
            => Mathf.Lerp(a, b, GetLerpValue(deltaTime, halfLifeTime));

        public static Vector3 SmoothLerp(Vector3 a, Vector3 b, float deltaTime, float timeToTarget, float precision = 0.01f) 
            => Vector3.Lerp(a, b, GetLerpValue(deltaTime, timeToTarget, precision));

        public static Vector3 SmoothLerp(Vector3 a, Vector3 b, float deltaTime, float halfLifeTime)
           => Vector3.Lerp(a, b, GetLerpValue(deltaTime, halfLifeTime));

        public static Vector2 SmoothLerp(Vector2 a, Vector2 b, float deltaTime, float timeToTarget, float precision = 0.01f) 
            => Vector2.Lerp(a, b, GetLerpValue(deltaTime, timeToTarget, precision));

        public static Vector2 SmoothLerp(Vector2 a, Vector2 b, float deltaTime, float halfLifeTime)
           => Vector2.Lerp(a, b, GetLerpValue(deltaTime, halfLifeTime));

        public static Quaternion SmoothSlerp(Quaternion a, Quaternion b, float deltaTime, float timeToTarget, float precision = 0.01f) 
            => Quaternion.Slerp(a, b, GetLerpValue(deltaTime, timeToTarget, precision));

        public static Quaternion SmoothLerp(Quaternion a, Quaternion b, float deltaTime, float halfLifeTime)
           => Quaternion.Slerp(a, b, GetLerpValue(deltaTime, halfLifeTime));

        private static float GetLerpValue(float deltaTime, float timeToTarget, float precision) 
            => 1 - Mathf.Pow(precision, (deltaTime / timeToTarget));

        private static float GetLerpValue(float deltaTime, float halfLifeTime) 
            => 1 - Mathf.Pow(2f, -(deltaTime / halfLifeTime));
    }
}