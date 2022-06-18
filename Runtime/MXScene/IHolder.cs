namespace Voxell.MotionGFX
{
  internal interface IHolder
  {
    float StartTime { get; }
    float Duration { get; }
    float EndTime { get; }

    void Evaluate(float globalTime);
  }
}