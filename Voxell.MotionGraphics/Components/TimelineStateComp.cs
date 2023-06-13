using Unity.Entities;

namespace Voxell.MotionGraphics
{
    public struct TimelineStateComp : IComponentData
    {
        public float Time;
        public bool Playing;
    }
}
