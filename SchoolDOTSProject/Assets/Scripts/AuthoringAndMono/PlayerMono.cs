using Unity.Entities;
using UnityEngine;

namespace DOTS
{
    public class PlayerMono : MonoBehaviour
    {
        public float PlayerHealth;
    }

    public class PlayerBaker : Baker<PlayerMono>
    {
        public override void Bake(PlayerMono authoring)
        {
            var playerEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PlayerTag>(playerEntity);
            AddComponent(playerEntity, new PlayerHealthComponent { Health = authoring.PlayerHealth, MaxHealth = authoring.PlayerHealth });
            AddBuffer<PlayerDamageBufferElement>(playerEntity);
        }
    }
}