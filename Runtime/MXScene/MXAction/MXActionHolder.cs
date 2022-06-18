using Unity.Mathematics;

namespace Voxell.MotionGFX
{
  public class MXActionHolder : IHolder
  {
    private MXAction.Act _action;

    public float StartTime => _startTime;
    private protected float _startTime;

    public float Duration => _duration;
    private protected float _duration;

    public float EndTime => _startTime + _duration;

    public float WaitDuration => _waitDuration;
    private float _waitDuration;

    public MXActionHolder(MXAction.Act action)
    {
      this._action = action;
      this._startTime = -1.0f;
      this._duration = 1.0f;
      this._waitDuration = 0.0f;
    }

    public MXActionHolder Animate(float duration)
    {
      _duration = math.max(duration, 0.0f);
      return this;
    }

    public MXActionHolder Wait(float duration = -1.0f)
    {
      if (duration == -1.0f) _waitDuration = _duration;
      else _waitDuration = math.max(duration, 0.0f);
      return this;
    }

    internal void SetStartTime(float startTime) => _startTime = startTime;

    public void Evaluate(float globalTime)
    {
      float t = CalculateTime(globalTime);
      _action(t);
    }

    /// <returns>Unit time value that is between the range of 0.0f and 1.0f</returns>
    private float CalculateTime(float globalTime)
    {
      // local time must be between action start and end time, so we clamp the time
      float localTime = math.clamp(
        globalTime, StartTime, EndTime
      ) - StartTime;

      // action time is evaulated with only unit values 0.0f -> 1.0f
      return localTime / Duration;
    }
  }
}