using Unity.Entities;
using UnityEngine;

namespace DOTS
{
    public class ObstacleMono : MonoBehaviour
    {
        
    }

    public class ObstacleBaker : Baker<ObstacleMono>
    {
        public override void Bake(ObstacleMono authoring)
        {
            var obstacleEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<CollisionTag>(obstacleEntity);
            AddComponent<CollisionProperties>(obstacleEntity, new CollisionProperties
            {
                Position = authoring.transform.position
            });
        }
    }
}