using Unity.Entities;

namespace DOTS
{
    public struct UnitAttackProperties : IComponentData, IEnableableComponent
    {
        public float AttackDamagePerSecond;
        public float AttackAmplitude;
        public float AttackFrequency;
    }
}
