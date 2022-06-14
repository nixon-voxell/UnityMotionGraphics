namespace Voxell.MotionGFX
{
  public class MXPauseAction : AbstractMXAction
  {
    public MXPauseAction(float animDuration) : base(animDuration) => Wait();

    // do nothing, we just wait
    public override void Evaluate(float clipTime) {}
  }
}