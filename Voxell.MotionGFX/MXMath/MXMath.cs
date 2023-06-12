using System.Runtime.CompilerServices;
using Unity.Burst;

namespace Voxell.MotionGFX
{
    [BurstCompile]
    public static partial class MXMath
    {
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float OneMinus(float x) => 1.0f - x;

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinusOne(float x) => x - 1.0f;

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DoubleMinus(float x) => -2.0f * x + 2.0f;
    }
}