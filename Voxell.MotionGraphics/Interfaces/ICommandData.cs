using Unity.Entities;

namespace Voxell.MotionGraphics
{
    public interface ICommandData<T> : IComponentData
    where T : unmanaged
    {
        public T Start { get; set; }
        public T End { get; set; }
    }
}
