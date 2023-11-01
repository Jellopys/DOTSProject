using System.Drawing;
using Unity.Burst.Intrinsics;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace DOTS
{
    public readonly partial struct UnitAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _transform;

        public LocalTransform GetTransform
        {
            get => _transform.ValueRO;
            set => _transform.ValueRW = value;
        }

        public float3 Position
        {
            get => _transform.ValueRO.Position;
            set => _transform.ValueRW.Position = value;
        }

        public void SetLocation(float3 location)
        {
            _transform.ValueRW.Position = location;
        }

        public void SetRotation(quaternion rotation)
        {
            _transform.ValueRW.Rotation = rotation;
        }

        public void DestroyUnit(EntityCommandBuffer ecb, Entity entity)
        {
            ecb.DestroyEntity(entity);
        }
    }
}