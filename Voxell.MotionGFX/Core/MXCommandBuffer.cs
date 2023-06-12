using Unity.Collections;
using Unity.Burst;

namespace Voxell.MotionGFX
{
    [BurstCompile]
    public struct MXCommandBuffer : System.IDisposable
    {
        private NativeList<MXCommand> m_MXCommands;

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
