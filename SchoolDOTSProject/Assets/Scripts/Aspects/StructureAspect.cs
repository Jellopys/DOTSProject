using Unity.Entities;
using Unity.Transforms;

namespace DOTS
{
    public readonly partial struct StructureAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _transform;
        private readonly RefRW<StructureHealthComponent> _structureHealth;
        private readonly DynamicBuffer<StructureDamageBufferElement> _structureDamageBuffer;

        public void DamageStructure()
        {
            foreach (var structureDamageBufferElement in _structureDamageBuffer)
            {
                _structureHealth.ValueRW.Health -= structureDamageBufferElement.Value;
            }
            _structureDamageBuffer.Clear();

            _transform.ValueRW.Scale = _structureHealth.ValueRO.Health / _structureHealth.ValueRO.MaxHealth;
        }
    }

    
}