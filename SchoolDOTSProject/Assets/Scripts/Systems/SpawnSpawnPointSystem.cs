using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;

namespace DOTS
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnSpawnPointSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GameModeProperties>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            var gameModeEntity = SystemAPI.GetSingletonEntity<GameModeProperties>();
            var gameMode = SystemAPI.GetAspect<GameModeAspect>(gameModeEntity);

            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var spawnPointOffset = new float3(0f, -2, 1);

            var builder = new BlobBuilder(Allocator.Temp); // this is how you build arrays?
            ref var spawnPoints = ref builder.ConstructRoot<UnitSpawnPointsBlob>();
            var arrayBuilder = builder.Allocate(ref spawnPoints.Value, gameMode.NumberSpawnPointsToPlace);

            for (var i = 0; i < gameMode.NumberSpawnPointsToPlace; i++)
            {
                var newSpawnPoint = ecb.Instantiate(gameMode.SpawnPointPrefab);
                var newSpawnPointTransform = gameMode.GetRandomSpawnPointTransform();
                ecb.SetComponent(newSpawnPoint, newSpawnPointTransform);

                var newUnitSpawnPoint = newSpawnPointTransform.Position + spawnPointOffset;
                arrayBuilder[i] = newUnitSpawnPoint;
            }

            var blobAsset = builder.CreateBlobAssetReference<UnitSpawnPointsBlob>(Allocator.Persistent);
            ecb.Playback(state.EntityManager);
        }
    }
}