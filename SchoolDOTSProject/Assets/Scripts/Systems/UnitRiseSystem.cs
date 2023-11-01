using Unity.Entities;
using Unity.Burst;

namespace DOTS
{
    [BurstCompile]
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(SpawnUnitSystem))]
    public partial struct UnitRiseSystem : ISystem
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

            new UnitRiseJob
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct UnitRiseJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;

        [BurstCompile]
        private void Execute(UnitRiseAspect unit, [ChunkIndexInQuery]int sortKey)
        {
            unit.Rise(DeltaTime);
            if (!unit.IsAboveGround) return;

            unit.SetAtGroundLevel();
            ECB.RemoveComponent<StructureTag>(sortKey, unit.Entity);
            ECB.SetComponentEnabled<UnitWalkProperties>(sortKey, unit.Entity, true);
        }   
    }
}