using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Burst;
using Unity.Entities;

namespace Voxell.MotionGraphics
{
    [StructLayout(LayoutKind.Auto)]
    public partial struct MotionCommandSystem : ISystem
    {
        public NativeList<MotionCommand> na_Commands;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<MotionConfigComp>();
            state.RequireForUpdate<TimelineStateComp>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            RefRW<TimelineStateComp> timelineStateComp = SystemAPI.GetSingletonRW<TimelineStateComp>();

            if (!timelineStateComp.ValueRO.Playing) return;

            timelineStateComp.ValueRW.Time += SystemAPI.Time.DeltaTime;
        }
    }
}
