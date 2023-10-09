using Unity.Entities;
using Unity.Mathematics;

namespace DOTS
{
    public struct GameModeProperties : IComponentData
    {
        public float2 FieldDimensions;
        public int NumberSpawnPointsToPlace;
        public Entity SpawnPointPrefab;
    }
}
