using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace DOTS
{
    public readonly partial struct PlayerAspect : IAspect
    {
        public readonly Entity Entity;
        private readonly RefRO<PlayerProperties.MousePosition> _mousePosition;
        private readonly RefRW<PlayerProperties.MouseRayHitPosition> _mouseRayHitPosition;
        private readonly RefRO<PlayerProperties.FireInput> _fireInput;
        private readonly RefRO<PlayerProperties.ProjectileEntity> _projectileEntity;
        private readonly RefRW<PlayerProperties.Fire> _fire;
        private readonly RefRW<PlayerProperties.MoveInput> _moveInput;
        private readonly RefRW<PlayerProperties.MoveSpeed> _moveSpeed;
        private readonly RefRW<LocalTransform> _transform;

        public Entity GetProjectileEntity => _projectileEntity.ValueRO.Value;

        public float ShootCooldown => _fire.ValueRO.ShootCooldown;
        public float ShootTimer
        {
            get => _fire.ValueRO.ShootTimer;
            set => _fire.ValueRW.ShootTimer = value;
        }

        public float2 GetMousePosition()
        {
            return _mousePosition.ValueRO.Value;
        }

        public void SetMousePosition(Vector3 rayLocation)
        {
            
            _mouseRayHitPosition.ValueRW.Value = rayLocation;
        }

        public bool FirePressed()
        {
            return _fireInput.ValueRO.Value;
        }

        public LocalTransform GetTransform
        {
            get => _transform.ValueRO;
            set => _transform.ValueRW = value;
        }

        public LocalTransform GetProjectileSpawnPoint()
        {
            var trans = new LocalTransform();
            trans.Position = GetTransform.Position;
            trans.Rotation = GetRotationFromMouseInput();
            trans.Scale = 0.4f;
            return trans;
        }

        public void SetLocation(float3 location)
        {
            Vector3 vector = location;
            if (vector.magnitude < 0f) location = 0f;
            var velocity = location /= 2; // dampening
            _transform.ValueRW.Position += velocity;
        }

        public void SetRotation(quaternion rotation)
        {
            _transform.ValueRW.Rotation = rotation;
        }

        public Vector3 GetLocationFromInput(float DeltaTime)
        {
            var input = new Vector2(_moveInput.ValueRO.Value.x, _moveInput.ValueRO.Value.y);
            input *= DeltaTime * _moveSpeed.ValueRO.Value;
            return new Vector3(input.x, 0, input.y);
        }

        public quaternion GetRotationFromMouseInput()
        {
            //var playerPos = new float2(_transform.ValueRO.Position.x, _transform.ValueRO.Position.z);

            //var screenCenter = new float2(Screen.width / 2, Screen.height / 2);
            //Vector2 adjustedInput = _mousePosition.ValueRO.Value - screenCenter;
            //adjustedInput.Normalize();


            var lookRotation =
                quaternion.LookRotationSafe(
                    new float3(_mouseRayHitPosition.ValueRO.Value), new float3(0,1,0));
            return lookRotation;
        }
    }
}