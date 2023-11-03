using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using UnityEngine;
using Ray = UnityEngine.Ray;
using Plane = UnityEngine.Plane;

namespace DOTS
{
    //[BurstCompile]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct PlayerMoveSystem : ISystem
    {

        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var player in SystemAPI.Query<PlayerAspect>()) // did it outside of job because cannot get Camera.main in job?
            {
                var playertEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
                Vector3 playerPos = player.GetTransform.Position;
                Vector2 mousePos = player.GetMousePosition();

                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                Plane groundPlane = new Plane(Vector3.up, playerPos);
                float rayDistance;
                Vector3 point = new Vector3(0, 0, 0);

                if (groundPlane.Raycast(ray, out rayDistance))
                {
                    point = ray.GetPoint(rayDistance);
                    point -= playerPos;

                    player.SetMousePosition(point);
                }
            }

            new MovePlayerJob
            {
                DeltaTime = deltaTime
            }.Schedule();
        }
    }

    //[BurstCompile]
    public partial struct MovePlayerJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        public void Execute(PlayerAspect aspect)
        {
            aspect.SetLocation(aspect.GetLocationFromInput(DeltaTime));
            aspect.SetRotation(aspect.GetRotationFromMouseInput());
        }
    }
}