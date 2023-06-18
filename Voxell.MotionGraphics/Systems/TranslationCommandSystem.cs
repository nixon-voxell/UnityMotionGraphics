using Unity.Collections;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Voxell.MotionGraphics
{
    [BurstCompile]
    public partial struct TranslationCommandSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TimelineComp>();
            state.RequireForUpdate<TranslationCommandComp>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
        }
    }
}
