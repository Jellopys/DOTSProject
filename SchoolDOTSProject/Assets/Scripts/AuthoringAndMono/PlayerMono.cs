using Unity.Entities;
using UnityEngine;

namespace DOTS
{
    public class PlayerMono : MonoBehaviour
    {
        
    }

    public class PlayerBaker : Baker<PlayerMono>
    {
        public override void Bake(PlayerMono authoring)
        {
            var playerEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PlayerTag>(playerEntity);
        }
    }
}