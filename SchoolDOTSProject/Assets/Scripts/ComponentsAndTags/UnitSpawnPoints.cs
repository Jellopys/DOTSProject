using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace DOTS
{
    public struct UnitSpawnPoints : IComponentData
    {
        public BlobAssetReference<UnitSpawnPointsBlob> Value;
    }

    public struct UnitSpawnPointsBlob
    {
        public BlobArray<float3> Value;
    }
}