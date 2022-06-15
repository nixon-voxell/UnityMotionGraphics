using UnityEngine;
using Unity.Mathematics;

namespace Voxell.MotionGFX
{
  public static partial class MXAction
  {
    public delegate void Act(float t);

    public static void PauseAct(float t) {}

    /// <summary>Move transfrom from a start position to an end position.</summary>
    public static Act Move(
      float3 startPos, float3 endPos, Transform transform, MXMath.Transition transition
    )
    {
      void MoveAct(float t) => transform.position = math.lerp(startPos, endPos, transition(t));
      return MoveAct;
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