using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Burst;

namespace Voxell.MotionGraphics
{
    using static Interpolation;

    public struct MotionCommand
    {
        public delegate void CommandDelegate(float time);

        public float Start;
        public float Duration;
        public FunctionPointer<CommandDelegate> Command;
        public FunctionPointer<InterpolationDelegate> Interpolation;

        public void Execute(float time)
        {
            float t = (time - this.Start) / this.Duration;

            Command.Invoke(Interpolation.Invoke(t));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FunctionPointer<CommandDelegate> CreateFuncPointer(CommandDelegate func)
        {
            System.IntPtr funcPtr = Marshal.GetFunctionPointerForDelegate<CommandDelegate>(func);
            return new FunctionPointer<CommandDelegate>(funcPtr);
        }
    }
}
