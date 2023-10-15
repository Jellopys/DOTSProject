

using Unity.Mathematics;
using UnityEditor;

namespace DOTS
{
    public static class MathHelpers
    {
        public static float GetHeading(float3 objectPosition, float3 targetPosition) // Get LookAtRotation
        {
            var x = objectPosition.x - targetPosition.x;
            var y = objectPosition.z - targetPosition.z;
            return math.atan2(x, y) + math.PI;
        }
    }
}
