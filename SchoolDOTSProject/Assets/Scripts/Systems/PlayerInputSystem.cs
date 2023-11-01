using Unity.Entities;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.Burst.Intrinsics;

namespace DOTS
{
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    [BurstCompile]
    public partial class PlayerInputSystem : SystemBase
    {

        private PlayerInputActions _actions;
        private Entity _playerEntity;

        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<PlayerTag>();
            RequireForUpdate<PlayerProperties.MousePosition>();
            RequireForUpdate<PlayerProperties.MoveInput>();
            RequireForUpdate<PlayerProperties.FireInput>();

            _actions = new PlayerInputActions();
        }

        [BurstCompile]
        protected override void OnStartRunning()
        {
            _actions.Enable();

            _playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            // Read input values
            var aimInput = _actions.Controls.Aim.ReadValue<Vector2>();
            var movementInput = _actions.Controls.Movement.ReadValue<Vector2>();
            var fire = _actions.Controls.Fire.IsPressed();

            SystemAPI.SetSingleton(new PlayerProperties.MousePosition()
            {
                Value = new float2(aimInput)
            });
            SystemAPI.SetSingleton(new PlayerProperties.MoveInput()
            {
                Value = new float2(movementInput)
            });
            SystemAPI.SetSingleton(new PlayerProperties.FireInput()
            {
                Value = fire
            });
        }

        [BurstCompile]
        protected override void OnStopRunning()
        {
            _playerEntity = Entity.Null;
            _actions.Disable();
        }
    }
}