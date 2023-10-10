using Unity.Entities;
using Unity.Mathematics;

namespace DOTS
{
    public struct GameModeProperties : IComponentData
    {
        public float2 FieldDimensions;
        public int NumberSpawnPointsToPlace;
        public Entity SpawnPointPrefab;
        public Entity UnitPrefab;
        public float UnitSpawnRate;
    }

    public struct UnitSpawnTimer : IComponentData
    {
        public float Value;
    }
}
