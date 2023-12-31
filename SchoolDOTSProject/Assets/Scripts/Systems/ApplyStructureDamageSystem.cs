using Unity.Entities;
using Unity.Burst;
using UnityEngine;

namespace DOTS
{
    //[BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    [UpdateAfter(typeof(EndSimulationEntityCommandBufferSystem))]
    public partial struct ApplyStructureDamageSystem : ISystem
    {
        //[BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Dependency.Complete();
            foreach (var structure in SystemAPI.Query<StructureAspect>())
            {
                structure.DamageStructure();
            }
        }
    }
}