using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;

namespace DOTS
{
    [BurstCompile]
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(UnitRiseSystem))]
    public partial struct UnitWalkSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<StructureTag>();
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
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
            var targetEntity = SystemAPI.GetSingletonEntity<StructureTag>();
            var targetScale = SystemAPI.GetComponent<LocalTransform>(targetEntity).Scale;
            var targetRadius = targetScale * 1f + 0.5f;

            new UnitWalkJob
            {
                DeltaTime = deltaTime,
                targetRadiusSq = targetRadius * targetRadius,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct UnitWalkJob : IJobEntity
    {
        public float DeltaTime;
        public float targetRadiusSq;
        public EntityCommandBuffer.ParallelWriter ECB;

        private void Execute(UnitWalkAspect unit, [ChunkIndexInQuery] int sortKey)
        {
            unit.Walk(DeltaTime);
            if (unit.IsInAttackRange(float3.zero, targetRadiusSq))
            {
                ECB.SetComponentEnabled<UnitWalkProperties>(sortKey, unit.Entity, false);
                ECB.SetComponentEnabled<UnitAttackProperties>(sortKey, unit.Entity, true);
            }
        }
    }
}