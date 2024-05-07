using UnityEngine;

namespace ExtraUtility
{
    public static class NumberExtentions
    {
        public static bool IsAproximatelyEqualTo(this float ourValue, float otherValue, float epsilon = 1E-10f) 
            => Mathf.Abs(ourValue - otherValue) <= epsilon;  
        
        public static bool IsAproximatelyZero(this float ourValue) 
            => Mathf.Abs(ourValue) <= Mathf.Epsilon;

        public static bool IsAproximatelyEqualTo(this Vector3 ourValue, Vector3 otherValue, float epsilon = 1E-10f)
            => ourValue.x.IsAproximatelyEqualTo (otherValue.x, epsilon)
            && ourValue.y.IsAproximatelyEqualTo (otherValue.y, epsilon)
            && ourValue.z.IsAproximatelyEqualTo(otherValue.z, epsilon);

        public static bool IsAproximatelyEqualTo(this Vector2 ourValue, Vector2 otherValue, float epsilon = 1E-10f)
            => ourValue.x.IsAproximatelyEqualTo(otherValue.x, epsilon)
            && ourValue.y.IsAproximatelyEqualTo(otherValue.y, epsilon);

        public static bool IsSquareNumber(this float value) 
            => (Mathf.Sqrt(value) % 1f).IsAproximatelyZero();  
        
        public static bool IsSquareNumber(this int value) 
            => (Mathf.Sqrt(value) % 1f).IsAproximatelyZero();
    }
}
