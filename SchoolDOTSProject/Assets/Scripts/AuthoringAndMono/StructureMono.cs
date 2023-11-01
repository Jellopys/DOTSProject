using Unity.Entities;
using UnityEngine;

namespace DOTS
{
    public class StructureMono : MonoBehaviour
    {
        public float StructureHealth;
    }

    public class StructureBaker : Baker<StructureMono>
    {
        public override void Bake(StructureMono authoring)
        {
            var structureEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<StructureTag>(structureEntity);
            AddComponent(structureEntity, new StructureHealthComponent { Health = authoring.StructureHealth, MaxHealth = authoring.StructureHealth });
            AddBuffer<StructureDamageBufferElement>(structureEntity);
        }
    }
}