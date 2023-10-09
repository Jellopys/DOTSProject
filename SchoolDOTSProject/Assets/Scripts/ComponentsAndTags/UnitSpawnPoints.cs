using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace DOTS
{
    public struct UnitSpawnPoints : IComponentData
    {
        public NativeArray<float3> Value;
    }
}