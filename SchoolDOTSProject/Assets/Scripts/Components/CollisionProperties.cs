using Unity.Entities;
using Unity.Mathematics;

namespace DOTS
{
    public struct CollisionProperties : IComponentData, IEnableableComponent
    {
        public float3 Position;
    }
}
