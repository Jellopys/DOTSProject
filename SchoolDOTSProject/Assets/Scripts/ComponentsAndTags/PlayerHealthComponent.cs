using Unity.Entities;
using Unity.Mathematics;

namespace DOTS
{
    public struct PlayerHealthComponent : IComponentData
    {
        public float Health;
        public float MaxHealth;
    }
}