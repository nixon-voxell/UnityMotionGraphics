using UnityEngine;
using Unity.Mathematics;

namespace Voxell.MotionGFX
{
    public static class A_RectTransform
    {
        public static MXAction SetTranslation(float2 trans, RectTransform rectTransform)
        {
            void _(float t) => rectTransform.anchoredPosition = trans;
            return _;
        }

        public static MXAction Translate(
            float2 x, float2 y, RectTransform rectTransform, MXMath.Transition transition
        ) {
            void _(float t) => rectTransform.anchoredPosition = math.lerp(x, y, transition(t));
            return _;
        }
    }
}