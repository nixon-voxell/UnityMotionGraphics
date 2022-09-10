using UnityEngine;
using Unity.Mathematics;

namespace Voxell.MotionGFX
{
    public class A_Transform
    {
        public static MXAction SetTranslation(float3 trans, Transform transform)
        {
            void _(float t) => transform.localPosition = trans;
            return _;
        }

        public static MXAction SetRotation(float3 rotation, Transform transform)
        {
            void _(float t) => transform.localRotation = quaternion.EulerXYZ(rotation);
            return _;
        }

        public static MXAction SetScale(float3 scale, Transform transform)
        {
            void _(float t) => transform.localScale = scale;
            return _;
        }

        public static MXAction Translate(
            float3 x, float3 y, Transform transform, MXMath.Transition transition
        ) {
            void _(float t) => transform.localPosition = math.lerp(x, y, transition(t));
            return _;
        }

        public static MXAction Rotate(
            float3 x, float3 y, Transform transform, MXMath.Transition transition
        ) {
            void _(float t) => transform.localRotation = quaternion.EulerXYZ(math.lerp(x, y, transition(t)));
            return _;
        }

        public static MXAction Scale(
            float3 x, float3 y, Transform transform, MXMath.Transition transition
        ) {
            void _(float t) => transform.localScale = math.lerp(x, y, transition(t));
            return _;
        }
    }
}