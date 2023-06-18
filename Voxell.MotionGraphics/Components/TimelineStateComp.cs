using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Entities;

namespace Voxell.MotionGraphics
{
    public struct TimelineComp : IComponentData, IEnableableComponent
    {
        // public NativeList<MotionCommand> na_Commands;

        [MarshalAs(UnmanagedType.U1)]
        public bool Playing;
        public float PrevTime;
        public float CurrTime;
    }
}
