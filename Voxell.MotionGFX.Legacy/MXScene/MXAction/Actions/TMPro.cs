using UnityEngine;
using Unity.Mathematics;
using TMPro;

namespace Voxell.MotionGFX
{
    public static class A_TMPro
    {
        public static MXAction SetColor(Color color, TextMeshProUGUI ugui)
        {
            void _(float t) => ugui.color = color;
            return _;
        }

        public static MXAction FadeColor(
            Color x, Color y, TextMeshProUGUI ugui, MXMath.Transition transition
        ) {
            void _(float t) => ugui.color = Color.Lerp(x, y, transition(t));
            return _;
        }

        public static MXAction SetAlpha(float alpha, TextMeshProUGUI ugui)
        {
            void _(float t)
            {
                Color uguiColor = ugui.color;
                uguiColor.a = alpha;
                ugui.color = uguiColor;
            }
            return _;
        }

        public static MXAction FadeAlpha(
            float x, float y, TextMeshProUGUI ugui, MXMath.Transition transition
        ) {
            void _(float t)
            {
                Color uguiColor = ugui.color;
                uguiColor.a = math.lerp(x, y, transition(t));
                ugui.color = uguiColor;
            }
            return _;
        }
    }
}