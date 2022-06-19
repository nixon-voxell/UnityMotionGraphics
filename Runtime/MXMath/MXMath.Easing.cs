using Unity.Mathematics;

namespace Voxell.MotionGFX
{
  public partial class MXMath
  {
    // data found at https://easings.net/
    public static float EaseInSine(float t) => 1 - math.cos((t * math.PI) * 0.5f);

    public static float EaseOutSine(float t) => math.sin((t * math.PI) * 0.5f);

    public static float EaseInOutSine(float t) => -(math.cos(math.PI * t) - 1) * 0.5f;

    public static readonly MXMath.Transition EaseInQuad =
      MXMath.CubicBezierTransition(0.11f, 0.0f, 0.5f, 0.0f);

    public static readonly MXMath.Transition EaseOutQuad =
      MXMath.CubicBezierTransition(0.5f, 1.0f, 0.89f, 1.0f);

    public static readonly MXMath.Transition EaseInOutQuad =
      MXMath.CubicBezierTransition(0.45f, 0.0f, 0.55f, 1.0f);

    public static readonly MXMath.Transition EaseInCubic =
      MXMath.CubicBezierTransition(0.32f, 0.0f, 0.67f, 0.0f);

    public static readonly MXMath.Transition EaseOutCubic =
      MXMath.CubicBezierTransition(0.33f, 1.0f, 0.68f, 1.0f);

    public static readonly MXMath.Transition EaseInOutCubic =
      MXMath.CubicBezierTransition(0.65f, 0.0f, 0.35f, 1.0f);

    public static readonly MXMath.Transition EaseInQuart =
      MXMath.CubicBezierTransition(0.5f, 0.0f, 0.75f, 0.0f);

    public static readonly MXMath.Transition EaseOutQuart =
      MXMath.CubicBezierTransition(0.25f, 1.0f, 0.5f, 1.0f);

    public static readonly MXMath.Transition EaseInOutQuart =
      MXMath.CubicBezierTransition(0.76f, 0.0f, 0.24f, 1.0f);

    public static readonly MXMath.Transition EaseInQuint =
      MXMath.CubicBezierTransition(0.64f, 0.0f, 0.78f, 0.0f);

    public static readonly MXMath.Transition EaseOutQuint =
      MXMath.CubicBezierTransition(0.22f, 1.0f, 0.36f, 1.0f);

    public static readonly MXMath.Transition EaseInOutQuint =
      MXMath.CubicBezierTransition(0.83f, 0.0f, 0.17f, 1.0f);

    public static readonly MXMath.Transition EaseInExpo =
      MXMath.CubicBezierTransition(0.7f, 0.0f, 0.84f, 0.0f);

    public static readonly MXMath.Transition EaseOutExpo =
      MXMath.CubicBezierTransition(0.16f, 1.0f, 0.3f, 1.0f);

    public static readonly MXMath.Transition EaseInOutExpo =
      MXMath.CubicBezierTransition(0.87f, 0.0f, 0.13f, 1.0f);

    public static readonly MXMath.Transition EaseInCirc =
      MXMath.CubicBezierTransition(0.55f, 0.0f, 1.0f, 0.45f);

    public static readonly MXMath.Transition EaseOutCirc =
      MXMath.CubicBezierTransition(0.0f, 0.55f, 0.45f, 1.0f);

    public static readonly MXMath.Transition EaseInOutCirc =
      MXMath.CubicBezierTransition(0.85f, 0.0f, 0.15f, 1.0f);

    public static readonly MXMath.Transition EaseInBack =
      MXMath.CubicBezierTransition(0.36f, 0.0f, 0.66f, -0.56f);

    public static readonly MXMath.Transition EaseOutBack =
      MXMath.CubicBezierTransition(0.34f, 1.56f, 0.64f, 1.0f);

    public static readonly MXMath.Transition EaseInOutBack =
      MXMath.CubicBezierTransition(0.68f, -0.6f, 0.32f, 1.6f);
  }
}