using Unity.Entities;
using Unity.Mathematics;

namespace DOTS
{
    public struct GameModeRandom : IComponentData
    {
        public Random Value;
    }
}