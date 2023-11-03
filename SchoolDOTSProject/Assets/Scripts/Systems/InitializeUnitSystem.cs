using Unity.Entities;
using Unity.Burst;
using Unity.Collections;

namespace DOTS
{
    //[BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct InitializeUnitSystem : ISystem
    {
        //[BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var unit in SystemAPI.Query<UnitWalkAspect>().WithAll<NewUnitTag>())
            {
                ecb.RemoveComponent<NewUnitTag>(unit.Entity);
                ecb.SetComponentEnabled<UnitWalkProperties>(unit.Entity, false);
                ecb.SetComponentEnabled<UnitAttackProperties>(unit.Entity, false);
            }
            ecb.Playback(state.EntityManager);
        }
    }

    //[BurstCompile]
    public partial struct InitializeUnitJob : IJobEntity
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
            //ECB.SetComponent(newUnit, new UnitHeading { Value = unitHeading });
        }   
    }
}