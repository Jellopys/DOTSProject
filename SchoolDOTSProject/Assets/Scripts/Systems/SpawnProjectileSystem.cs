using Unity.Entities;
using Unity.Burst;
using UnityEngine;

namespace DOTS
{
    [BurstCompile]
    public partial struct SpawnProjectileSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var playerAspect in SystemAPI.Query<PlayerAspect>())
            {
                if (playerAspect.FirePressed())
                {
                    var deltaTime = SystemAPI.Time.DeltaTime;
                    var ecbParallel = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

                    new FireProjectileJob
                    {
                        DeltaTime = deltaTime,
                        ECB = ecbParallel.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
                    }.ScheduleParallel();
                }
                    
            }
        }

    }

    [BurstCompile]
    public partial struct FireProjectileJob : IJobEntity
    {

        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;

        [BurstCompile]
        public void Execute(PlayerAspect playerAspect, [EntityIndexInQuery] int sortKey)
        {
            var projectile = ECB.Instantiate(sortKey, playerAspect.GetProjectileEntity);
            ECB.SetComponent(sortKey, projectile, playerAspect.GetProjectileSpawnPoint());
        }

    }
}