using Unity.Entities;
using Unity.Mathematics;

namespace DOTS
{
    public struct StructureHealthComponent : IComponentData
    {
        public float Health;
        public float MaxHealth;
    }
}