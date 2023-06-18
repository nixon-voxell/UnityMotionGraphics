using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Transforms;

namespace Voxell.MotionGraphics
{
    public struct TranslationCommandComp : ICommand<LocalTransform>, ICommandData<float3>
    {
        public float3 Start { get; set; }
        public float3 End { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Execute(float time, RefRW<LocalTransform> transform)
        {
            transform.ValueRW.Position = math.lerp(this.Start, this.End, time);
        }
    }
}
