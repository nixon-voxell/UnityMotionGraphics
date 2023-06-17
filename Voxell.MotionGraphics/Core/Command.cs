using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Entities;
using Unity.Burst;

namespace Voxell.MotionGraphics
{
    [BurstCompile]
    public static partial class Command
    {
        public delegate void CommandDelegate(float time);

        /// <summary>The amount of time that has passed since the last command.</summary>
        public static float ElapsedTime { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FunctionPointer<CommandDelegate> CreateFuncPointer(CommandDelegate func)
        {
            System.IntPtr funcPtr = Marshal.GetFunctionPointerForDelegate<CommandDelegate>(func);
            return new FunctionPointer<CommandDelegate>(funcPtr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResetElapsedTime()
        {
            ElapsedTime = 0.0f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IncrementElapsedTime(float amount)
        {
            ElapsedTime += amount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Command CreateCommand<Comp, Command, T>(
            RefRW<Comp> component, T start, T end
        )
        where Comp: unmanaged, IComponentData
        where Command : unmanaged, ICommandData<Comp, T>
        where T : unmanaged
        {
            return new Command
            {
                Component = component,
                Start = start,
                End = end,
            };
        }
    }
}
