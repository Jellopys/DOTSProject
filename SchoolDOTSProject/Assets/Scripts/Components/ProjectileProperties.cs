using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace DOTS
{
    public struct ProjectileProperties : IComponentData
    {
        public float ProjectileMaxDistance;
        public float ProjectileSpeed;
        public float ProjectileLifeTime;
    }
}
