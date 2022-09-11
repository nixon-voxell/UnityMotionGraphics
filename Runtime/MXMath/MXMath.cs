using System.Runtime.CompilerServices;

namespace Voxell.MotionGFX
{
    public static partial class MXMath
    {
        public delegate float Transition(float t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float OneMinus(float t) => 1.0f - t;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinusOne(float t) => t - 1.0f;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DoubleMinus(float t) => -2.0f * t + 2.0f;
    }
}