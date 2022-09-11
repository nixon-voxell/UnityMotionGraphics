using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Voxell.MotionGFX
{
    public partial class MXMath
    {
        // data found at https://easings.net/

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Linear(float t) => t;

        // sine
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInSine(float t) => 1 - math.cos((t * math.PI) * 0.5f);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseOutSine(float t) => math.sin((t * math.PI) * 0.5f);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInOutSine(float t) => -(math.cos(math.PI * t) - 1) * 0.5f;

        // quad
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInQuad(float t) => t * t;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseOutQuad(float t)
        {
            float a = OneMinus(t);
            return 1.0f - a * a;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInOutQuad(float t)
        {
            float a = DoubleMinus(t);
            return t < 0.5f ? 2 * t * t : 1.0f - (a * a) * 0.5f;
        }

        // cubic
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInCubic(float t) => t * t * t;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseOutCubic(float t)
        {
            float a = OneMinus(t);
            return 1.0f - a * a * a;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInOutCubic(float t)
        {
            float a = DoubleMinus(t);
            return t < 0.5f ? 4.0f * t * t * t : 1.0f - a * a * a * 0.5f;
        }

        // quart
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInQuart(float t) => t * t * t * t;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseOutQuart(float t)
        {
            float a = OneMinus(t);
            return 1.0f - a * a * a * a;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInOutQuart(float t)
        {
            float a = DoubleMinus(t);
            return t < 0.5 ? 8.0f * t * t * t * t : 1.0f - a * a * a * a * 0.5f;
        }

        // quint
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInQuint(float t) => t * t * t * t * t;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseOutQuint(float t)
        {
            float a = OneMinus(t);
            return 1.0f - a * a * a * a * a;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInOutQuint(float t)
        {
            float a = DoubleMinus(t);
            return t < 0.5f ? 16.0f * t * t * t * t * t : 1.0f - a * a * a * a * a * 0.5f;
        }

        // expo
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInExpo(float t) => math.pow(2.0f, 10.0f * t - 10.0f);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseOutExpo(float t) => 1.0f - math.pow(2.0f, -10.0f * t);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInOutExpo(float t)
            => t < 0.5f ? math.pow(2.0f, 20.0f * t - 10.0f) * 0.5f : (2.0f - math.pow(2.0f, -20.0f * t + 10.0f)) * 0.5f;

        // circ
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInCirc(float t) => 1.0f - math.sqrt(1.0f - t * t);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseOutCirc(float t)
        {
            float a = MinusOne(t);
            return math.sqrt(1.0f - a * a);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInOutCirc(float t)
        {
            float a = 2.0f * t;
            float b = DoubleMinus(t);
            return t < 0.5f ? (1.0f - math.sqrt(1.0f - a * a)) * 0.5f : (math.sqrt(1.0f - b * b) + 1.0f) * 0.5f;
        }

        // back
        private const float C1 = 1.70158f;
        private const float C3 = C1 + 1.0f;
        private const float C2 = C1 * 1.525f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInBack(float t) => C3 * t * t * t - C1 * t * t;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseOutBack(float t)
        {
            float a = MinusOne(t);
            return 1.0f + C3 * a * a * a + C1 * a * a;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float EaseInOutBack(float t)
        {
            float a = 2.0f * t;
            float b = 2.0f * t - 2.0f;
            return t < 0.5f ? (a * a * ((C2 + 1.0f) * a - C2)) * 0.5f : (b * b * ((C2 + 1.0f) * b + C2) + 2.0f) * 0.5f;
        }
    }
}