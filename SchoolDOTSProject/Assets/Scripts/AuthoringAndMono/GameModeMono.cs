using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

namespace DOTS
{    
    public class GameModeMono : MonoBehaviour
    {
        public float2 FieldDimensions;
        public int NumberSpawnPointsToPlace;
        public GameObject SpawnPointPrefab;
        public uint RandomSeed;
        public GameObject UnitPrefab;
        public float UnitSpawnRate;
    }

    public class GameModeBaker : Baker<GameModeMono>
    {
        public override void Bake(GameModeMono authoring)
        {
            var gameModeEntity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(gameModeEntity, new GameModeProperties
            {
                FieldDimensions = authoring.FieldDimensions,
                NumberSpawnPointsToPlace = authoring.NumberSpawnPointsToPlace,
                SpawnPointPrefab = GetEntity(authoring.SpawnPointPrefab, TransformUsageFlags.Dynamic),
                UnitPrefab = GetEntity(authoring.UnitPrefab, TransformUsageFlags.Dynamic),
                UnitSpawnRate = authoring.UnitSpawnRate
            });
            AddComponent(gameModeEntity, new GameModeRandom
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });
            AddComponent<UnitSpawnPoints>(gameModeEntity);
            AddComponent<UnitSpawnTimer>(gameModeEntity);
        }
    }
}