using UnityEngine;

namespace ExtraUtility
{
    public static class VectorExtensions
    {
        public static Vector2Int ToVector2Int(this Vector2 @this) => new Vector2Int((int)@this.x, (int)@this.y);

        public static Vector2 ToVector2(this (float x, float y) @this) => new Vector2Int((int)@this.x, (int)@this.y);

        public static Vector2Int ToVector2Int(this (int x, int y) @this) => new Vector2Int(@this.x, @this.y);

        public static Vector3 ToVector3(this Vector2 @this, float zVal = 0) => new Vector3(@this.x, @this.y, zVal);

        public static Vector3 ToVector3(this Vector2Int @this, float zVal = 0) => new Vector3(@this.x, @this.y, zVal);

        public static Vector3 ToVector3(this Vector3Int @this, float zVal = 0) => new Vector3(@this.x, @this.y, zVal);

        public static Vector3 ToVector3(this (float x, float y) @this, float zVal = 0) => new Vector3(@this.x, @this.y, zVal);

        public static Vector3 ToVector3(this (float x, float y, float z) @this) => new Vector3(@this.x, @this.y, @this.z);

        public static Vector3 ToVector3(this (int x, int y, int z) @this) => new Vector3(@this.x, @this.y, @this.z);

        public static Vector3Int ToVector3Int(this (int x, int y, int z) @this) => new Vector3Int(@this.x, @this.y, @this.z);

        public static Vector3Int ToVector3Int(this (int x, int y) @this, int zVal = 0) => new Vector3Int(@this.x, @this.y, zVal);

        public static Vector3Int ToVector3Int(this (float x, float y) @this, int zVal = 0) => new Vector3Int((int)@this.x, (int)@this.y, zVal);

        public static Vector3Int ToVector3Int(this (float x, float y, float z) @this) => new Vector3Int((int)@this.x, (int)@this.y, (int)@this.z);

        public static Vector3Int ToVector3Int(this Vector2 @this, int zVal = 0) => new Vector3Int((int)@this.x, (int)@this.y, zVal);

        public static Vector3Int ToVector3Int(this Vector2Int @this, int zVal = 0) => new Vector3Int(@this.x, @this.y, zVal);

        public static Vector3Int ToVector3Int(this Vector3 @this) => new Vector3Int((int)@this.x, (int)@this.y, (int)@this.z);

         public static void Deconstruct(this Vector2 @this, out float x, out float y) { x = @this.x; y = @this.y; }
        public static void Deconstruct(this Vector2Int @this, out int x, out int y) { x = @this.x; y = @this.y; }

        public static void Deconstruct(this Vector3 @this, out float x, out float y, out float z) { x = @this.x; y = @this.y; z = @this.z; }
        public static void Deconstruct(this Vector3Int @this, out int x, out int y, out int z) { x = @this.x; y = @this.y; z = @this.z; }


        // ReSharper disable InconsistentNaming
        public static Vector2 xy(this Vector3 @this) => new Vector2(@this.x, @this.y);

        public static Vector2 xz(this Vector3 @this) => new Vector2(@this.x, @this.z);

        public static Vector2 yz(this Vector3 @this) => new Vector2(@this.y, @this.z);

        public static Vector2 yx(this Vector3 @this) => new Vector2(@this.y, @this.x);
        public static Vector2 zx(this Vector3 @this) => new Vector2(@this.z, @this.x);
        public static Vector2 zy(this Vector3 @this) => new Vector2(@this.z, @this.y);

        public static Vector2 zyx(this Vector3 @this) => new Vector3(@this.z, @this.y, @this.x);
        public static Vector2 xy0(this Vector3 @this) => new Vector3(@this.x, @this.y, 0);
        public static Vector2 x0z(this Vector3 @this) => new Vector3(@this.x, 0, @this.z);
        public static Vector2 _0yz(this Vector3 @this) => new Vector3(0, @this.y, @this.z);

        public static Vector2Int xyInt(this Vector3 @this) => new Vector2Int((int)@this.x, (int)@this.y);

        public static Vector2Int xzInt(this Vector3 @this) => new Vector2Int((int)@this.x, (int)@this.z);

        public static Vector2Int yzInt(this Vector3 @this) => new Vector2Int((int)@this.y, (int)@this.z);

        public static Vector2Int xy(this Vector3Int @this) => new Vector2Int(@this.x, @this.y);

        public static Vector2Int xz(this Vector3Int @this) => new Vector2Int(@this.x, @this.z);

        public static Vector2Int yz(this Vector3Int @this) => new Vector2Int(@this.y, @this.z);



        public static (float x, float y) xy(this (float x, float y, float z) @this) => (@this.x, @this.y);

        public static (float x, float z) xz(this (float x, float y, float z) @this) => (@this.x, @this.z);

        public static (float y, float z) yz(this (float x, float y, float z) @this) => (@this.y, @this.z);

        public static (int x, int y) xy(this (int x, int y, int z) @this) => (@this.x, @this.y);

        public static (int x, int z) xz(this (int x, int y, int z) @this) => (@this.x, @this.z);

        public static (int y, int z) yz(this (int x, int y, int z) @this) => (@this.y, @this.z);



        // ReSharper restore InconsistentNaming
    }
}
