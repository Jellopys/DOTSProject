using UnityEngine;
using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;

namespace DOTS
{
    [BurstCompile]
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
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
            var targetEntity = SystemAPI.GetSingletonEntity<StructureTag>();
            var targetScale = SystemAPI.GetComponent<LocalTransform>(targetEntity).Scale;
            var targetRadius = targetScale * 1f + 1f;

            new UnitAttackJob
            {
                DeltaTime = deltaTime,
                TargetRadiusSq = targetRadius * targetRadius,
                StructureEntity = targetEntity,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct UnitAttackJob : IJobEntity
    {
        public float DeltaTime;
        public Entity StructureEntity;
        public float TargetRadiusSq;
        public EntityCommandBuffer.ParallelWriter ECB;

        private void Execute(UnitAttackAspect unit, [ChunkIndexInQuery] int sortKey)
        {
            if (unit.IsInAttackRange(float3.zero, TargetRadiusSq)) 
            {
                unit.Attack(DeltaTime, ECB, sortKey, StructureEntity);
            }
            else
            {
                ECB.SetComponentEnabled<UnitAttackProperties>(sortKey, unit.Entity, false);
                ECB.SetComponentEnabled<UnitWalkProperties>(sortKey, unit.Entity, true);
            }

        }
    }

}