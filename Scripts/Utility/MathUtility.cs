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

        public static Vector2 RandomVector2(float min, float max)
        {
            return new Vector2(Random.Range(min, max), Random.Range(min, max));
        }
        
        public static Vector3 RandomVector2AsVector3(float min, float max)
        {
            return new Vector3(Random.Range(min, max), Random.Range(min, max));
        }
    }
}