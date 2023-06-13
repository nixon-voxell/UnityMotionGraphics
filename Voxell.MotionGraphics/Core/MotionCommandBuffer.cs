using Unity.Collections;
using Unity.Burst;

namespace Voxell.MotionGraphics
{
    [BurstCompile]
    public struct MXCommandBuffer : System.IDisposable
    {
        private NativeList<MotionCommand> m_MXCommands;

        [BurstCompile]
        public void Execute()
        {
            
        }

        public void Dispose()
        {
            this.m_MXCommands.Dispose();
        }
    }
}
