using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.Burst;

namespace Voxell.MotionGraphics
{
    [BurstCompile]
    public static partial class MotionMath
    {
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float OneMinus(float x)
        {
            return 1.0f - x;
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float MinusOne(float x)
        {
            return x - 1.0f;
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DoubleMinus(float x)
        {
            return -2.0f * x + 2.0f;
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CubicBezier(float p0x, float p0y, float p1x, float p1y, float t, out float2 p)
        {
            CubicBezier(new float2(p0x, p0y), new float2(p1x, p1y), t, out p);
        }

        [BurstCompile]
        public static void CubicBezier(in float2 p0, in float2 p1, float t, out float2 p)
        {
            float2 a = math.lerp(0.0f, p0, t);
            float2 b = math.lerp(p0, p1, t);
            float2 c = math.lerp(p1, 1.0f, t);

            float2 d = math.lerp(a, b, t);
            float2 e = math.lerp(b, c, t);
            p = math.lerp(d, e, t).y;
        }
    }
}