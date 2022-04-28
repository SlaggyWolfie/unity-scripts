using UnityEngine;

namespace Slaggy.Unity.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 ModifyX(this Vector3 vector3, float x)
        {
            vector3.x = x;
            return vector3;
        }

        public static Vector3 ModifyY(this Vector3 vector3, float y)
        {
            vector3.y = y;
            return vector3;
        }

        public static Vector3 ModifyZ(this Vector3 vector3, float z)
        {
            vector3.z = z;
            return vector3;
        }

        public static Vector2 FlipXY(this Vector2 vector2) => new Vector2(vector2.y, vector2.x);

        public static Vector2 XY(this Vector3 vector3) => new Vector2(vector3.x, vector3.y);
        public static Vector2 XZ(this Vector3 vector3) => new Vector2(vector3.x, vector3.z);
        public static Vector2 YZ(this Vector3 vector3) => new Vector2(vector3.y, vector3.z);

        public static Vector3 ToXY(this Vector2 vector2) => vector2;
        public static Vector3 ToXZ(this Vector2 vector2) => new Vector3(vector2.x, 0, vector2.y);
        public static Vector3 ToYZ(this Vector2 vector2) => new Vector3(0, vector2.x, vector2.y);

        public static Vector4 AsPosition(this Vector3 vector3) => new Vector4(vector3.x, vector3.y, vector3.z, 1);
        public static Vector4 AsDirection(this Vector3 vector3) => new Vector4(vector3.x, vector3.y, vector3.z, 0);
        public static Vector3 AsVector3(this Vector4 vector4) => new Vector3(vector4.x, vector4.y, vector4.z);
        
        public static Vector2 Clamp(Vector2 current, Vector2 min, Vector2 max)
        {
            current.x = Mathf.Clamp(current.x, min.x, max.x);
            current.y = Mathf.Clamp(current.y, min.y, max.y);
            return current;
        }

        public static Vector3 Clamp(Vector3 current, Vector3 min, Vector3 max)
        {
            current.x = Mathf.Clamp(current.x, min.x, max.x);
            current.y = Mathf.Clamp(current.y, min.y, max.y);
            current.z = Mathf.Clamp(current.z, min.z, max.z);
            return current;
        }

        public static Vector3 LerpToTarget(Vector3 start, Vector3 end, float factor, float minDistance)
        {
            Vector3 lerp = Vector3.Lerp(start, end, factor);
            return (end - lerp).sqrMagnitude < minDistance * minDistance ? end : lerp;
        }
    }
}