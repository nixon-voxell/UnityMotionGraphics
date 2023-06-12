using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Entities;
using Unity.Burst;

namespace Voxell.MotionGFX
{
    [BurstCompile]
    public static class Command
    {
        public delegate void CommandDelegate(float time);

        public static FunctionPointer<CommandDelegate> CreateCommandFuncPointer(CommandDelegate func)
        {
            System.IntPtr funcPtr = Marshal.GetFunctionPointerForDelegate<CommandDelegate>(func);
            return new FunctionPointer<CommandDelegate>(funcPtr);
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
