using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DOTS
{
    public readonly partial struct UnitRiseAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRO<UnitRiseRate> _unitRiseRate;

        public void Rise(float DeltaTime)
        {
            _transform.ValueRW.Position += math.up() * _unitRiseRate.ValueRO.Value * DeltaTime;
        }

        public bool IsAboveGround => _transform.ValueRO.Position.y >= 0f;

        public void SetAtGroundLevel()
        {
            var position = _transform.ValueRO.Position;
            position.y = 0f;
            _transform.ValueRW.Position = position;
        }
    }
}