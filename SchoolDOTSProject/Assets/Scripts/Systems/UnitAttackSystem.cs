using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;

namespace DOTS
{
    [BurstCompile]
    [UpdateAfter(typeof(UnitWalkSystem))]
    public partial struct UnitAttackSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var targetEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var targetScale = SystemAPI.GetComponent<LocalTransform>(targetEntity).Scale;
            var targetRadius = targetScale * 1f + 1f;

            new UnitAttackJob
            {
                DeltaTime = deltaTime,
                TargetRadiusSq = targetRadius * targetRadius,
                PlayerEntity = targetEntity,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct UnitAttackJob : IJobEntity
    {
        public float DeltaTime;
        public Entity PlayerEntity;
        public float TargetRadiusSq;
        public EntityCommandBuffer.ParallelWriter ECB;

        private void Execute(UnitAttackAspect unit, [ChunkIndexInQuery] int sortKey)
        {
            if (unit.IsInAttackRange(float3.zero, TargetRadiusSq)) // refactor this to player position
            {
                unit.Attack(DeltaTime, ECB, sortKey, PlayerEntity);
            }
            else
            {
                ECB.SetComponentEnabled<UnitAttackProperties>(sortKey, unit.Entity, false);
                ECB.SetComponentEnabled<UnitWalkProperties>(sortKey, unit.Entity, true);
            }
            
        }   
    }
}