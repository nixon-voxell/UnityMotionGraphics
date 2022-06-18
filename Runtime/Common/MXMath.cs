using Unity.Mathematics;

namespace Voxell.MotionGFX
{
  public static class MXMath
  {
    public delegate float Transition(float t);

    public static float EaseInSine(float t)
    {
      return 1 - math.cos((t * math.PI) * 0.5f);
    }

    public static float EaseOutSine(float t)
    {
      return math.sin((t * math.PI) * 0.5f);
    }

    public static float EaseInOutSine(float t)
    {
      return -(math.cos(math.PI * t) - 1) * 0.5f;
    }

    public static readonly MXMath.Transition EaseInOutExpo =
      MXMath.CubicBezierTransition(0.87f, 0.0f, 0.13f, 1.0f);

    public static Transition CubicBezierTransition(float p0x, float p0y, float p1x, float p1y)
    {
      float CBTrans(float t) => CubicBezier(p0x, p0y, p1x, p1y, t);
      return CBTrans;
    }

    public static Transition CubicBezierTransition(float2 p0, float2 p1)
    {
      float CBTrans(float t) => CubicBezier(p0, p1, t);
      return CBTrans;
    }

    public static float CubicBezier(float p0x, float p0y, float p1x, float p1y, float t)
    {
      return CubicBezier(new float2(p0x, p0y), new float2(p1x, p1y), t);
    }

    public static float CubicBezier(float2 p0, float2 p1, float t)
    {
      float2 a = math.lerp(0.0f, p0, t);
      float2 b = math.lerp(p0, p1, t);
      float2 c = math.lerp(p1, 1.0f, t);

      float2 d = math.lerp(a, b, t);
      float2 e = math.lerp(b, c, t);
      return math.lerp(d, e, t).y;
    }
  }
}