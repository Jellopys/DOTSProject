using Unity.Entities;
using UnityEngine;

namespace DOTS
{
    public class UnitMono : MonoBehaviour
    {
        public float RiseRate;
        public float WalkSpeed;
        public float WalkAmplitude;
        public float WalkFrequency;

        public float AttackDamagePerSec;
        public float AttackAmplitude;
        public float AttackFrequency;
    }

    public class UnitBaker : Baker<UnitMono>
    {
        public override void Bake(UnitMono authoring)
        {
            var unitEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(unitEntity, new UnitRiseRate { Value = authoring.RiseRate });
            AddComponent(unitEntity, new UnitWalkProperties
            {
                WalkSpeed = authoring.WalkSpeed,
                WalkAmplitude = authoring.WalkAmplitude,
                WalkFrequency = authoring.WalkFrequency
            });
            AddComponent(unitEntity, new UnitAttackProperties
            {
                AttackDamagePerSecond = authoring.AttackDamagePerSec,
                AttackAmplitude = authoring.AttackAmplitude,
                AttackFrequency = authoring.AttackFrequency
            });
            AddComponent<UnitTimer>(unitEntity);
            AddComponent<UnitHeading>(unitEntity);
            AddComponent<NewUnitTag>(unitEntity);
        }
    }
}