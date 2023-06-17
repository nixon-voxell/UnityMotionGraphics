using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Burst;
using Unity.Entities;

namespace Voxell.MotionGraphics
{
    [StructLayout(LayoutKind.Auto)]
    public partial struct MotionCommandSystem : ISystem, System.IDisposable
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<MotionConfigComp>();
            state.RequireForUpdate<TimelineComp>();

            state.EntityManager.AddComponentData<TimelineComp>(
                state.SystemHandle, new TimelineComp
                {
                    na_Commands = new NativeList<MotionCommand>(1024, Allocator.Persistent),
                    Playing = true,
                    PrevTime = 0.0f,
                    CurrTime = 0.0f,
                }
            );
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            RefRW<TimelineComp> timelineComp = SystemAPI.GetSingletonRW<TimelineComp>();

            TimelineComp timelineCompVal = timelineComp.ValueRO;
            MotionGraphicsEngine.ExecuteCommand(ref timelineCompVal, ref timelineCompVal.na_Commands);

            // update time values
            timelineComp.ValueRW.PrevTime = timelineComp.ValueRO.CurrTime;
            timelineComp.ValueRW.CurrTime += SystemAPI.Time.DeltaTime;
        }

        public void Dispose()
        {
            TimelineComp timelineComp = SystemAPI.GetSingleton<TimelineComp>();
            if (timelineComp.na_Commands.IsCreated)
            {
                timelineComp.na_Commands.Dispose();
            }
        }
    }
}
