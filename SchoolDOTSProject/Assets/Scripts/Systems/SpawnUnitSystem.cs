using Unity.Entities;
using Unity.Burst;

namespace DOTS
{
    //[BurstCompile]
    public partial struct SpawnUnitSystem : ISystem
    { 
        //[BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbParallel = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

            new SpawnUnitJob
            {
                DeltaTime = deltaTime,
                ECB = ecbParallel.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()

            }.ScheduleParallel();
        }
    }

    //[BurstCompile]
    public partial struct SpawnUnitJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;

        private void Execute(GameModeAspect gameMode, [EntityIndexInQuery] int sortKey)
        {
            gameMode.UnitSpawnTimer -= DeltaTime;
            if (!gameMode.TimeToSpawnUnit) return;
            if (!gameMode.UnitSpawnPointInitialized()) return;

            gameMode.UnitSpawnTimer = gameMode.UnitSpawnRate;
            var newUnit = ECB.Instantiate(sortKey, gameMode.UnitPrefab);

            var newUnitTransform = gameMode.GetUnitSpawnPoint();
            ECB.SetComponent(sortKey, newUnit, newUnitTransform);

            var unitHeading = MathHelpers.GetHeading(newUnitTransform.Position, gameMode.Position);
            ECB.SetComponent(sortKey,newUnit, new UnitHeading { Value = unitHeading });
        }
    }
}