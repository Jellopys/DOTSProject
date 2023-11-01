using Unity.Entities;
using UnityEngine;

namespace DOTS
{
    public class PlayerMono : MonoBehaviour
    {
        public float MoveSpeed;
        public float RotationSpeed;
        public float ShootSpeed;
        public float ShootCooldown;
        public int Health;
        public GameObject ProjectilePrefab;
    }

    public class PlayerBaker : Baker<PlayerMono>
    {
        public override void Bake(PlayerMono authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerProperties.MoveSpeed
            {
                Value = authoring.MoveSpeed
            });
            AddComponent(entity, new PlayerProperties.RotationSpeed
            {
                Value = authoring.RotationSpeed
            });
            AddComponent(entity, new PlayerProperties.ShootSpeed
            {
                Value = authoring.ShootSpeed
            });
            AddComponent(entity, new PlayerProperties.ProjectileEntity
            {
                Value = GetEntity(authoring.ProjectilePrefab, TransformUsageFlags.Dynamic)
            });
            AddComponent(entity, new PlayerProperties.Fire
            {
                ShootCooldown = authoring.ShootCooldown
            });
            AddComponent(entity, new PlayerProperties.Health
            {
                Value = authoring.Health
            });

            AddComponent<PlayerTag>(entity);
            AddComponent<PlayerProperties.MousePosition>(entity);
            AddComponent<PlayerProperties.PlayerPosition>(entity);
            AddComponent<PlayerProperties.MoveInput>(entity);
            AddComponent<PlayerProperties.FireInput>(entity);
            AddComponent<PlayerProperties.MouseRayHitPosition>(entity);
        }
    }
}