using System.Globalization;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace DOTS
{
    public readonly partial struct ProjectileAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRW<ProjectileProperties> _projectileProperties;
        private readonly DynamicBuffer<ProjectileLifeTimeBufferElement> _projectileLifeTimeBuffer;

        private float ProjectileSpeed => _projectileProperties.ValueRO.ProjectileSpeed;

        public void TravelForward(float deltaTime)
        {
            _transform.ValueRW.Position += _transform.ValueRO.Forward() * ProjectileSpeed * deltaTime;
        }

        public float3 Position
        {
            get => _transform.ValueRO.Position;
            set => _transform.ValueRW.Position = value;
        }
        public quaternion Rotation
        {
            get => _transform.ValueRO.Rotation;
            set => _transform.ValueRW.Rotation = value;
        }

        public bool HasLifeTimeLeft()
        {
            if (_projectileProperties.ValueRW.ProjectileLifeTime <= 0)
            {
                return false;
            }
            return true;
        }

        public void DestroyProjectile(EntityCommandBuffer ecb, Entity entity)
        {
            ecb.DestroyEntity(entity);
        }

        public void DestroyProjectile(EntityCommandBuffer.ParallelWriter ecb, Entity entity, int sortKey)
        {
            ecb.DestroyEntity(sortKey, entity);
        }

        public void CountLifetime(EntityCommandBuffer.ParallelWriter ecb, Entity entity, int sortKey, float deltaTime)
        {
            var lifeTimeBuffer = new ProjectileLifeTimeBufferElement { Value = deltaTime };

            ecb.AppendToBuffer(sortKey, entity, lifeTimeBuffer);

            foreach (var _projectileLifeTimeBufferElement in _projectileLifeTimeBuffer)
            {
                _projectileProperties.ValueRW.ProjectileLifeTime -= _projectileLifeTimeBufferElement.Value;
            }
            
            _projectileLifeTimeBuffer.Clear();
        }
    }


}