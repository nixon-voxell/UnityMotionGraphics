using Unity.Burst;

using static Unity.Mathematics.math;

namespace Voxell.MotionGraphics
{
    using static MotionMath;

    [BurstCompile]
    public static class Interpolation
    {
        /// <summary>Return eased time given a unit time (0..1)</summary>
        public delegate float InterpolationDelegate(float time);

        // ==================================
        // data found at https://easings.net/
        // ==================================

        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float Linear(float t)
        {
            return t;
        }

        // ==================================
        // sine
        // ==================================
        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseInSine(float t)
        {
            return 1 - cos((t * PI) * 0.5f);
        }

        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseOutSine(float t)
        {
            return sin((t * PI) * 0.5f);
        }

        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseInOutSine(float t)
        {
            return -(cos(PI * t) - 1) * 0.5f;
        }

        // ==================================
        // quad
        // ==================================
        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseInQuad(float t)
        {
            return t * t;
        }

        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseOutQuad(float t)
        {
            float a = OneMinus(t);
            return 1.0f - a * a;
        }

        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseInOutQuad(float t)
        {
            float a = DoubleMinus(t);
            return t < 0.5f ? 2 * t * t : 1.0f - (a * a) * 0.5f;
        }

        // ==================================
        // cubic
        // ==================================
        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseInCubic(float t)
        {
            return t * t * t;
        }

        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseOutCubic(float t)
        {
            float a = OneMinus(t);
            return 1.0f - a * a * a;
        }

        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseInOutCubic(float t)
        {
            float a = DoubleMinus(t);
            return t < 0.5f ? 4.0f * t * t * t : 1.0f - a * a * a * 0.5f;
        }

        // ==================================
        // quart
        // ==================================
        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseInQuart(float t)
        {
            return t * t * t * t;
        }

        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseOutQuart(float t)
        {
            float a = OneMinus(t);
            return 1.0f - a * a * a * a;
        }

        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseInOutQuart(float t)
        {
            float a = DoubleMinus(t);
            return t < 0.5 ? 8.0f * t * t * t * t : 1.0f - a * a * a * a * 0.5f;
        }

        // ==================================
        // quint
        // ==================================
        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseInQuint(float t)
        {
            return t * t * t * t * t;
        }

        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseOutQuint(float t)
        {
            float a = OneMinus(t);
            return 1.0f - a * a * a * a * a;
        }

        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseInOutQuint(float t)
        {
            float a = DoubleMinus(t);
            return t < 0.5f ? 16.0f * t * t * t * t * t : 1.0f - a * a * a * a * a * 0.5f;
        }

        // ==================================
        // expo
        // ==================================
        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseInExpo(float t)
        {
            return pow(2.0f, 10.0f * t - 10.0f);
        }

        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseOutExpo(float t)
        {
            return 1.0f - pow(2.0f, -10.0f * t);
        }

        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseInOutExpo(float t)
        {
            return t < 0.5f ? pow(2.0f, 20.0f * t - 10.0f) * 0.5f : (2.0f - pow(2.0f, -20.0f * t + 10.0f)) * 0.5f;
        }

        // ==================================
        // circ
        // ==================================
        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseInCirc(float t)
        {
            return 1.0f - sqrt(1.0f - t * t);
        }

        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseOutCirc(float t)
        {
            float a = MinusOne(t);
            return sqrt(1.0f - a * a);
        }

        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseInOutCirc(float t)
        {
            float a = 2.0f * t;
            float b = DoubleMinus(t);
            return t < 0.5f ? (1.0f - sqrt(1.0f - a * a)) * 0.5f : (sqrt(1.0f - b * b) + 1.0f) * 0.5f;
        }

        // ==================================
        // back
        // ==================================
        private const float C1 = 1.70158f;
        private const float C3 = C1 + 1.0f;
        private const float C2 = C1 * 1.525f;

        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseInBack(float t)
        {
            return C3 * t * t * t - C1 * t * t;
        }

        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseOutBack(float t)
        {
            float a = MinusOne(t);
            return 1.0f + C3 * a * a * a + C1 * a * a;
        }

        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(InterpolationDelegate))]
        public static float EaseInOutBack(float t)
        {
            float a = 2.0f * t;
            float b = 2.0f * t - 2.0f;
            return t < 0.5f ? (a * a * ((C2 + 1.0f) * a - C2)) * 0.5f : (b * b * ((C2 + 1.0f) * b + C2) + 2.0f) * 0.5f;
        }
    }
}
