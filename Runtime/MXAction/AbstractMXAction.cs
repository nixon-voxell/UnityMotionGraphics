namespace Voxell.MotionGFX
{
  public abstract class AbstractMXAction
  {
    public float WaitDuration => _waitDuration;
    public float AnimDuration => _animDuration;

    public float StartTime => _startTime;
    private float _startTime;

    public float EndTime => _startTime + _animDuration;

    private protected float _animDuration = 0.0f;
    private protected float _waitDuration = 0.0f;
    private protected Transition transition;

    public AbstractMXAction(float animDuration)
    {
      this._animDuration = animDuration;
    }

    public void Wait(float waitDuration = -1.0f)
      => _waitDuration = waitDuration == -1.0f ? _animDuration : waitDuration;

    /// <summary>Evaluate the actions to be taken at a given clip time.</summary>
    /// <param name="clipTime">Time relative to the clip.</param>
    public abstract void Evaluate(float clipTime);

    public void SetStartTime(float startTime) => _startTime = startTime;

    public delegate float Transition(float t);
  }
}