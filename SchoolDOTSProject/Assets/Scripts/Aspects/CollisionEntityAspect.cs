using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DOTS
{
    public readonly partial struct CollisionEntityAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<CollisionProperties> _transform;

        public float3 Position
        {
            get => _transform.ValueRO.Position;
            set => _transform.ValueRW.Position = value;
        }

        public void SetPosition(float3 location)
        {
            _transform.ValueRW.Position = location;
        }

        public void DestroyEntity(EntityCommandBuffer ecb, Entity entity)
        {
            ecb.DestroyEntity(entity);
        }
    }
}