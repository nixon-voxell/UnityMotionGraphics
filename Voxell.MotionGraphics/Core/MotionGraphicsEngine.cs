using Unity.Collections;
using Unity.Burst;

using static Unity.Mathematics.math;

namespace Voxell.MotionGraphics
{
    [BurstCompile]
    public static class MotionGraphicsEngine
    {
        /// <summary>Execute command forward or backward depending on time change from previous frame.</summary>
        [BurstCompile]
        public static void ExecuteCommand(ref TimelineComp timelineStateComp, ref NativeList<MotionCommand> na_commands)
        {
            float prevTime = timelineStateComp.PrevTime;
            float currTime = timelineStateComp.CurrTime;

            float minTime = min(prevTime, currTime);
            float maxTime = max(prevTime, currTime);

            float diffTime = currTime - prevTime;

            if (diffTime >= 0.0f)
            {
                ExecuteCommandForward(ref na_commands, minTime, maxTime, currTime);
            } else
            {
                ExecuteCommandBackward(ref na_commands, minTime, maxTime, currTime);
            }
        }

        /// <summary>Execute command in a forward manner.</summary>
        [BurstCompile]
        public static void ExecuteCommandForward(
            ref NativeList<MotionCommand> na_commands,
            float minTime, float maxTime, float currTime
        ) {
            for (int c = 0; c < na_commands.Length; c++)
            {
                MotionCommand command = na_commands[c];

                // only execute if command is in range
                if (MotionMath.IntersectRange(minTime, maxTime, command.Start, command.End))
                {
                    command.Execute(currTime);
                }
            }
        }

        /// <summary>Execute command in a backward manner.</summary>
        [BurstCompile]
        public static void ExecuteCommandBackward(
            ref NativeList<MotionCommand> na_commands,
            float minTime, float maxTime, float currTime
        ) {
            for (int c = na_commands.Length - 1; c >= 0 ; c--)
            {
                MotionCommand command = na_commands[c];

                // only execute if command is in range
                if (MotionMath.IntersectRange(minTime, maxTime, command.Start, command.End))
                {
                    command.Execute(currTime);
                }
            }
        }

        [BurstCompile]
        public static void ResetTimelineState(ref TimelineComp timelineStateComp)
        {
            timelineStateComp = new TimelineComp
            {
                Playing = false,
                PrevTime = 0.0f,
                CurrTime = 0.0f,
            };
        }
    }
}
