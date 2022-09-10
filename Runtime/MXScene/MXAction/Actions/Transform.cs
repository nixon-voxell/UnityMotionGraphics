using UnityEngine;
using Unity.Mathematics;

namespace Voxell.MotionGFX
{
  public class A_Transform
  {
    /// <summary>Set the translation of the transfrom.</summary>
    public static MXAction SetTranslation(float3 trans, Transform transform)
    {
      void _(float t) => transform.position = trans;
      return _;
    }

    /// <summary>Set the rotation of the transfrom.</summary>
    public static MXAction SetRotation(float3 rotation, Transform transform)
    {
      void _(float t) => transform.rotation = quaternion.EulerXYZ(rotation);
      return _;
    }

    /// <summary>Set the scale of the transform</summary>
    public static MXAction SetScale(float3 scale, Transform transform)
    {
      void _(float t) => transform.localScale = scale;
      return _;
    }

    /// <summary>Translate transfrom from a start position to an end position.</summary>
    public static MXAction Translate(
      ref float3 origin, float3 translation, Transform transform, MXMath.Transition transition
    )
    {
      float3 x = origin;
      float3 y = origin + translation;
      void _(float t) => transform.position = math.lerp(x, y, transition(t));
      origin = y;
      return _;
    }

    /// <summary>Translate transfrom from a start position to an end position.</summary>
    public static MXAction Translate(
      float3 x, float3 y, Transform transform, MXMath.Transition transition
    )
    {
      void _(float t) => transform.position = math.lerp(x, y, transition(t));
      return _;
    }

    /// <summary>Rotate transfrom from a start euler angle to an end euler angle.</summary>
    public static MXAction Rotate(
      ref float3 origin, float3 rotation, Transform transform, MXMath.Transition transition
    )
    {
      float3 x = origin;
      float3 y = origin + rotation;
      void _(float t) => transform.rotation = quaternion.EulerXYZ(math.lerp(x, y, transition(t)));
      origin = y;
      return _;
    }

    /// <summary>Rotate transfrom from a start euler angle to an end euler angle.</summary>
    public static MXAction Rotate(
      float3 x, float3 y, Transform transform, MXMath.Transition transition
    )
    {
      void _(float t) => transform.rotation = quaternion.EulerXYZ(math.lerp(x, y, transition(t)));
      return _;
    }

    /// <summary>Scale transfrom from a start scale to an end scale.</summary>
    public static MXAction Scale(
      ref float3 origin, float3 scale, Transform transform, MXMath.Transition transition
    )
    {
      float3 x = origin;
      float3 y = origin + scale;
      void _(float t) => transform.localScale = math.lerp(x, y, transition(t));
      origin = y;
      return _;
    }

    /// <summary>Scale transfrom from a start scale to an end scale.</summary>
    public static MXAction Scale(
      float3 x, float3 y, Transform transform, MXMath.Transition transition
    )
    {
      void _(float t) => transform.localScale = math.lerp(x, y, transition(t));
      return _;
    }
  }
}