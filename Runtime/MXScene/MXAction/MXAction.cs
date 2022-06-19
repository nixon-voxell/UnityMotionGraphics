using UnityEngine;
using Unity.Mathematics;

namespace Voxell.MotionGFX
{
  public static partial class MXAction
  {
    public delegate void Act(float t);

    public static void PauseAct(float t) {}

    /// <summary>Translate transfrom from a start position to an end position.</summary>
    public static Act SetTranslation(float3 trans, Transform transform)
    {
      void SetTranslationAct(float t) => transform.position = trans;
      return SetTranslationAct;
    }

    /// <summary>Translate transfrom from a start position to an end position.</summary>
    public static Act Translate(
      ref float3 startTrans, float3 endTrans, Transform transform, MXMath.Transition transition
    )
    {
      float3 _startTrans = startTrans;
      void TranslateAct(float t) => transform.position = math.lerp(_startTrans, endTrans, transition(t));
      startTrans = endTrans;
      return TranslateAct;
    }

    /// <summary>Rotate transfrom from a start euler angle to an end euler angle.</summary>
    public static Act Rotate(
      float3 startEuler, float3 endEuler, Transform transform, MXMath.Transition transition
    )
    {
      void RotateAct(float t) => transform.rotation = quaternion.EulerXYZ(
        math.lerp(startEuler, endEuler, transition(t))
      );
      return RotateAct;
    }

    /// <summary>Scale transfrom from a start scale to an end scale.</summary>
    public static Act Scale(
      float3 startScale, float3 endScale, Transform transform, MXMath.Transition transition
    )
    {
      void ScaleAct(float t) => transform.localScale = math.lerp(startScale, endScale, transition(t));
      return ScaleAct;
    }
  }
}