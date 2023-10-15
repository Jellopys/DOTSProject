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
            var targetRadius = targetScale * 1f + 0.5f;

            new UnitAttackJob
            {
                DeltaTime = deltaTime,
                targetRadiusSq = targetRadius * targetRadius,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct UnitAttackJob : IJobEntity
    {
        public float DeltaTime;
        public float targetRadiusSq;
        public EntityCommandBuffer.ParallelWriter ECB;

        private void Execute(UnitWalkAspect unit, [ChunkIndexInQuery] int sortKey)
        {
            // unit.Attack(DeltaTime);
            
        }   
    }
}