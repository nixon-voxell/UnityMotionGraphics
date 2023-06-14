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

        public void Execute(float time)
        {
            float t = (time - this.Start) / this.Duration;

            Command.Invoke(Interpolation.Invoke(t));
        }
    }
}
