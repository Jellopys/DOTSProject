using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Physics.Systems;
using Unity.Physics;
using UnityEngine;

namespace DOTS
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    public partial struct TriggerEventsSystem : ISystem
    {

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndVariableRateSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<SimulationSingleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //var ecb = SystemAPI.GetSingleton<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>();

            foreach (var projectile in SystemAPI.Query<ProjectileAspect>().WithAll<ProjectileTag>())
            {
                foreach (var unit in SystemAPI.Query<UnitWalkAspect>().WithAll<CollisionTag>())
                {
                    var buffer = SystemAPI.GetSingleton<EndVariableRateSimulationEntityCommandBufferSystem.Singleton>();
                    if (projectile.Entity != unit.Entity)
                    {

                        if (math.distance(projectile.Position.x, unit.Position.x) < 1f &&
                            math.distance(projectile.Position.z, unit.Position.z) < 1f)
                        {
                            projectile.DestroyProjectile(buffer.CreateCommandBuffer(state.WorldUnmanaged), projectile.Entity);

                            unit.DestroyUnit(buffer.CreateCommandBuffer(state.WorldUnmanaged), unit.Entity);
                        }
                    }
                }
            }

            //var ecb = SystemAPI.GetSingleton<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>();
            //foreach (var projectile in SystemAPI.Query<ProjectileAspect>().WithAll<ProjectileTag>())
            //{
            //    foreach (var entity in SystemAPI.Query<CollisionEntityAspect>().WithAll<CollisionTag>())
            //    {
            //        var buffer = SystemAPI.GetSingleton<EndVariableRateSimulationEntityCommandBufferSystem.Singleton>();
            //        if (projectile.Entity != entity.Entity)
            //        {

            //            if (math.distance(projectile.Position.x, entity.Position.x) < 1f &&
            //                math.distance(projectile.Position.z, entity.Position.z) < 1f)
            //            {
            //                projectile.DestroyProjectile(buffer.CreateCommandBuffer(state.WorldUnmanaged), projectile.Entity);

            //                entity.DestroyEntity(buffer.CreateCommandBuffer(state.WorldUnmanaged), entity.Entity);
            //            }
            //        }
            //    }
            //}

            
        }
    }
}