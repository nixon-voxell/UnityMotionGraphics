using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Burst;
using Unity.Entities;

namespace Voxell.MotionGraphics
{
    [StructLayout(LayoutKind.Auto)]
    public partial struct MotionCommandSystem : ISystem, System.IDisposable
    {
        /// <summary>Collection of all MotionCommands</summary>
        public NativeList<MotionCommand> na_Commands;
        /// <summary>Command indices that is being processed in the previous execution (frame).</summary>
        public NativeList<int> na_PrevProcessedCommands;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<MotionConfigComp>();
            state.RequireForUpdate<TimelineStateComp>();

            this.na_Commands =  new NativeList<MotionCommand>(1024, Allocator.Persistent);
            this.na_PrevProcessedCommands = new NativeList<int>(128, Allocator.Persistent);
            // initialize previous command with the first element
            this.na_PrevProcessedCommands.Add(0);
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            RefRW<TimelineStateComp> timelineStateComp = SystemAPI.GetSingletonRW<TimelineStateComp>();

            if (!timelineStateComp.ValueRO.Playing) return;

            // first process all previous MotionCommands
            for (int pc = 0; pc < this.na_PrevProcessedCommands.Length; pc++)
            {
                
            }

            // then search for potential MotionCommands to be process in this frame

            timelineStateComp.ValueRW.Time += SystemAPI.Time.DeltaTime;
        }

        public void Dispose()
        {
            this.na_Commands.Dispose();
            this.na_PrevProcessedCommands.Dispose();
        }
    }
}
