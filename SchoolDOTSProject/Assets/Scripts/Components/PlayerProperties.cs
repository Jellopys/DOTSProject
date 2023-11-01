using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace DOTS
{
    public struct PlayerProperties : IComponentData
    {
        public partial struct MousePosition : IComponentData
        {
            public float2 Value;
        }

        public partial struct MouseRayHitPosition : IComponentData
        {
            public float3 Value;
        }

        public partial struct MoveSpeed : IComponentData
        {
            public float Value;
        }

        public partial struct PlayerPosition : IComponentData
        {
            public float2 Value;
        }

        public partial struct MoveInput : IComponentData
        {
            public float2 Value;
        }

        public partial struct RotationSpeed : IComponentData
        {
            public float Value;
        }

        public partial struct ShootSpeed : IComponentData
        {
            public float Value;
        }

        public partial struct ProjectileEntity : IComponentData
        {
            public Entity Value;
        }

        public partial struct FireInput : IComponentData
        {
            public bool Value;
        }

        public partial struct Fire : IComponentData
        {
            public float ShootTimer;
            public float ShootCooldown;
        }

        public partial struct Health : IComponentData
        {
            public int Value;
        }
    }
}
