using Unity.Entities;
using Unity.Burst;
using UnityEngine;
using System.Globalization;

namespace DOTS
{
    //[BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    [UpdateAfter(typeof(EndSimulationEntityCommandBufferSystem))]
    [UpdateAfter(typeof(UnitRiseSystem))]
    public partial struct ProjectileLifeTimeSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        //[BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();

            new ProjectileLifeTimeJob
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter()
            }.ScheduleParallel();

            state.Dependency.Complete();
        }
    }

    //[BurstCompile]
    public partial struct ProjectileLifeTimeJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;

        private void Execute(ProjectileAspect projectile, [ChunkIndexInQuery] int sortKey)
        {
            projectile.CountLifetime(ECB, projectile.Entity, sortKey, DeltaTime);

            if (!projectile.HasLifeTimeLeft())
            {
                projectile.DestroyProjectile(ECB, projectile.Entity, sortKey);
            }
        }
    }
}