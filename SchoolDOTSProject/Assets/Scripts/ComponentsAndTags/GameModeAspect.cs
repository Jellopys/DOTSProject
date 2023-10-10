using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DOTS
{
    public readonly partial struct GameModeAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRO<LocalTransform> _transform;
        private readonly RefRO<GameModeProperties> _gameModeProperties;
        private readonly RefRW<GameModeRandom> _gameModeRandom;
        private readonly RefRW<UnitSpawnPoints> _unitSpawnPoints;

        private LocalTransform Transform => _transform.ValueRO;
        private int UnitSpawnPointCount => _unitSpawnPoints.ValueRO.Value.Value.Value.Length; // wtf syntax?
        public int NumberSpawnPointsToPlace => _gameModeProperties.ValueRO.NumberSpawnPointsToPlace;
        public Entity SpawnPointPrefab => _gameModeProperties.ValueRO.SpawnPointPrefab;        

        public bool UnitSpawnPointInitialized()
        {
            return _unitSpawnPoints.ValueRO.Value.IsCreated && UnitSpawnPointCount > 0;
        }

        public LocalTransform GetRandomSpawnPointTransform()
        {
            return new LocalTransform
            {
                Position = GetRandomPosition(),
                Rotation = GetRandomRotation(),
                Scale = GetRandomScale(0.5f)
            };
            
        }

        private float3 GetRandomPosition()
        {
            float3 randomPosition;
            do
            {
                randomPosition = _gameModeRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
            } while (math.distancesq(Transform.Position, randomPosition) <= BRAIN_SAFETY_RADIUS_SQ);

            return randomPosition;
        }

        private float3 MinCorner => Transform.Position - HalfDimensions;
        private float3 MaxCorner => Transform.Position + HalfDimensions;
        private float3 HalfDimensions => new()
        {
            x = _gameModeProperties.ValueRO.FieldDimensions.x * 0.5f,
            y = 0f,
            z = _gameModeProperties.ValueRO.FieldDimensions.y * 0.5f
        };
        private const float BRAIN_SAFETY_RADIUS_SQ = 100;

        private float GetRandomScale(float min) => _gameModeRandom.ValueRW.Value.NextFloat(min, 1f);
        private quaternion GetRandomRotation() => quaternion.RotateY(_gameModeRandom.ValueRW.Value.NextFloat(-0.25f, 0.25f));
    }
}