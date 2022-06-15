using Unity.Mathematics;

namespace Voxell.MotionGFX
{
  public class MXActionHolder
  {
    public MXAction.Act action;

    public float StartTime => _startTime;
    private float _startTime;

    public float WaitDuration => _waitDuration;
    private float _waitDuration;

    public float AnimDuration => _animDuration;
    private float _animDuration;

    public float EndTime => _startTime + _animDuration;

    public MXActionHolder(MXAction.Act action)
    {
      this.action = action;
      this._startTime = -1.0f;
      this._waitDuration = 1.0f;
      this._animDuration = 1.0f;
    }

    public MXActionHolder Animate(float duration)
    {
      _animDuration = math.max(duration, 0.0f);
      return this;
    }

    public MXActionHolder Wait(float duration = -1.0f)
    {
      if (duration == -1.0f) _waitDuration = _animDuration;
      else _waitDuration = math.max(duration, 0.0f);
      return this;
    }

    public void SetStartTime(float startTime) => _startTime = startTime;
  }
}