using System.Runtime.CompilerServices;
using Unity.Entities;

namespace Voxell.MotionGraphics
{
    public interface ICommand<Comp>
    where Comp: unmanaged, IComponentData
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Execute(float time, RefRW<Comp> comp);
    }
}
