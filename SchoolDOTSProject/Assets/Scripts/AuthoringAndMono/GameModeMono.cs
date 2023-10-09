using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Random = Unity.Mathematics.Random;

namespace DOTS
{    
    public class GameModeMono : MonoBehaviour
    {
        public float2 FieldDimensions;
        public int NumberSpawnPointsToPlace;
        public GameObject SpawnPointPrefab;
        public uint RandomSeed;
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
                SpawnPointPrefab = GetEntity(authoring.SpawnPointPrefab, TransformUsageFlags.Dynamic)
            });
            AddComponent(gameModeEntity, new GameModeRandom
            {
                Value = Random.CreateFromIndex(authoring.RandomSeed)
            });
            AddComponent<UnitSpawnPoints>();
        }
    }
}