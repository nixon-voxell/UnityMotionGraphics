using Unity.Burst;

namespace Voxell.MotionGFX
{
    public struct MXCommand
    {
        public float Start;
        public float Duration;
        public FunctionPointer<Command.CommandDelegate> Command;
        public FunctionPointer<Interpolation.InterpolationDelegate> Interpolation;

        public void Execute(float time)
        {
            float t = (time - this.Start) / this.Duration;

            Command.Invoke(Interpolation.Invoke(t));
        }
    }
}
