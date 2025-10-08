using UnityEngine;

namespace TryliomUtility
{
    public static class MathUtility
    {
        public static Vector3 GetDirection(Vector2 normalizedDirection, float addedAngle = 0f)
        {
            var angle = Mathf.Atan2(normalizedDirection.y, normalizedDirection.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle + addedAngle, Vector3.forward);

            return rotation * Vector2.right;
        }
        
        public static Vector2 GetDirection2D(float angle)
        {
            return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        }

        public static Vector3 GetDirection3D(float angle)
        {
            return Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
        }

        public static Vector2 RandomVec2(float min, float max)
        {
            return new Vector2(Random.Range(min, max), Random.Range(min, max));
        }
        
        public static Vector2 RandomVec2(float range)
        {
            return new Vector2(Random.Range(-range, range), Random.Range(-range, range));
        }
        
        public static Vector3 RandomVec2AsVec3(float min, float max)
        {
            return new Vector3(Random.Range(min, max), Random.Range(min, max));
        }
        
        public static Vector3 RandomVec2AsVec3(float range)
        {
            return new Vector3(Random.Range(-range, range), Random.Range(-range, range));
        }
    }
}