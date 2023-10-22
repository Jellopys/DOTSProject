using Unity.Entities;
using Unity.Transforms;

namespace DOTS
{
    public readonly partial struct PlayerAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRW<PlayerHealthComponent> _playerHealth;
        private readonly DynamicBuffer<PlayerDamageBufferElement> _playerDamageBuffer;

        public void DamagePlayer()
        {
            foreach (var playerDamageBufferElement in _playerDamageBuffer)
            {
                _playerHealth.ValueRW.Health -= playerDamageBufferElement.Value;
            }
            _playerDamageBuffer.Clear();

            _transform.ValueRW.Scale = _playerHealth.ValueRO.Health / _playerHealth.ValueRO.MaxHealth;

        }
    }

    
}