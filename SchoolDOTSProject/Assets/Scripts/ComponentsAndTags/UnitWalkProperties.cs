using Unity.Entities;

namespace DOTS
{
    public struct UnitWalkProperties : IComponentData, IEnableableComponent
    {
        public float WalkSpeed;
        public float WalkAmplitude;
        public float WalkFrequency;
    }

    public struct UnitTimer : IComponentData
    {
        public float Value;
    }

    public struct UnitHeading : IComponentData
    {
        public float Value;
    }

    public struct NewUnitTag : IComponentData
    {

    }
}
