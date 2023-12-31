using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace DOTS
{
    public readonly partial struct UnitAttackAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRW<UnitTimer> _unitTimer;
        private readonly RefRO<UnitAttackProperties> _attackProperties;
        private readonly RefRO<UnitHeading> _heading;

        private float AttackDamagePerSecond => _attackProperties.ValueRO.AttackDamagePerSecond;
        private float AttackAmplitude => _attackProperties.ValueRO.AttackAmplitude;
        private float AttackFrequency => _attackProperties.ValueRO.AttackFrequency;
        private float Heading => _heading.ValueRO.Value;

        private float UnitAttackTimer
        {
            get => _unitTimer.ValueRO.Value;
            set => _unitTimer.ValueRW.Value = value;
        }

        public void Attack(float deltaTime, EntityCommandBuffer.ParallelWriter ecb, int sortKey, Entity targetEntity)
        {
            UnitAttackTimer += deltaTime;
            var attackAngle = AttackAmplitude * math.sin(AttackFrequency * UnitAttackTimer);
            _transform.ValueRW.Rotation = quaternion.Euler(attackAngle, Heading, 0);

            var attackDamage = AttackDamagePerSecond * deltaTime;
            var curDamage = new StructureDamageBufferElement { Value = attackDamage };
            ecb.AppendToBuffer(sortKey, targetEntity, curDamage);
        }

        public bool IsInAttackRange(float3 targetPosition, float targetRadiusSq)
        {
            return math.distancesq(targetPosition, _transform.ValueRO.Position) <= targetRadiusSq - 1;
        }
    }
}