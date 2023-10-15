using Unity.Entities;
using Unity.Burst;

namespace DOTS
{
    [BurstCompile]
    public partial struct SpawnUnitSystem : ISystem
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
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();


            new SpawnUnitJob
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Run();
        }
    }

    [BurstCompile]
    public partial struct SpawnUnitJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer ECB;

        private void Execute(GameModeAspect gameMode)
        {
            gameMode.UnitSpawnTimer -= DeltaTime;
            if (!gameMode.TimeToSpawnUnit) return;
            if (!gameMode.UnitSpawnPointInitialized()) return;

            gameMode.UnitSpawnTimer = gameMode.UnitSpawnRate;
            var newUnit = ECB.Instantiate(gameMode.UnitPrefab);

            var newUnitTransform = gameMode.GetUnitSpawnPoint();
            ECB.SetComponent(newUnit, newUnitTransform);

            var unitHeading = MathHelpers.GetHeading(newUnitTransform.Position, gameMode.Position);
            ECB.SetComponent(newUnit, new UnitHeading { Value = unitHeading });
        }   
    }
}