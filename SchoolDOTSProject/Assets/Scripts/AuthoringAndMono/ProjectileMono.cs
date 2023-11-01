using Unity.Entities;
using UnityEngine;

namespace DOTS
{
    public class ProjectileMono : MonoBehaviour
    {
        public float ProjectileSpeed;
        public float ProjectileMaxDistance;
        public float ProjectileMaxLifeTime;
    }

    public class ProjectileBaker : Baker<ProjectileMono>
    {
        public override void Bake(ProjectileMono authoring)
        {
            var projectileEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(projectileEntity, new ProjectileProperties()
            {
                ProjectileSpeed = authoring.ProjectileSpeed,
                ProjectileMaxDistance = authoring.ProjectileMaxDistance,
                ProjectileLifeTime = authoring.ProjectileMaxLifeTime,
            });
            AddBuffer<ProjectileLifeTimeBufferElement>(projectileEntity);
            AddComponent<ProjectileTag>(projectileEntity);
            
        }
    }
}