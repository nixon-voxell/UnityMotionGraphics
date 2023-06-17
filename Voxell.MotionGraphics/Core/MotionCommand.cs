using Unity.Mathematics;
using Unity.Burst;

namespace Voxell.MotionGraphics
{
    using static Command;
    using static Interpolation;

    public struct MotionCommand
    {
        public float Start;
        public float Duration;
        public FunctionPointer<CommandDelegate> Command;
        public FunctionPointer<InterpolationDelegate> Interpolation;

        public float End => this.Start + this.Duration;

        public void Execute(float time)
        {
            time = (time - this.Start) / this.Duration;

            // make sure t is within the unit value
            time = math.clamp(time, 0.0f, 1.0f);
            Command.Invoke(Interpolation.Invoke(time));
        }

        public static void Play(
            FunctionPointer<CommandDelegate> command,
            FunctionPointer<InterpolationDelegate> interpolation
        ) {
            
        }
    }
}
