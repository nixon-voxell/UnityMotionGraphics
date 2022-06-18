using System.Collections.Generic;
using Unity.Mathematics;

namespace Voxell.MotionGFX
{
  public class MXSequence : ISeqHolder
  {
    List<IHolder> ISeqHolder.Holders => _holders;
    private protected List<IHolder> _holders;

    float IHolder.StartTime => _startTime;
    private protected float _startTime;

    float IHolder.Duration => _duration;
    private protected float _duration;

    float IHolder.EndTime => _startTime + _duration;

    float ISeqHolder.PrevGlobalTime { get; set; }

    public MXSequence()
    {
      this._holders = new List<IHolder>();
    }

    #region Sequence Creation

    public MXActionHolder Play(MXAction.Act action)
    {
      MXActionHolder actionHolder = new MXActionHolder(action);
      _holders.Add(actionHolder);

      return actionHolder;
    }

    public void Pause(float duration) => Play(MXAction.PauseAct).Animate(duration).Wait(duration);

    #endregion

    /// <returns>Total duration that the sequence uses.</returns>
    internal float CalculateDuration(float startTime)
    {
      _startTime = startTime;
      _duration = 0.0f;

      // setting up all start time is being assigned
      for (int h=0, holderCount=_holders.Count; h < holderCount; h++)
      {
        MXActionHolder actionHolder = _holders[h] as MXActionHolder;

        actionHolder.SetStartTime(_duration);
        _duration += actionHolder.WaitDuration;
      }

      // making sure that the duration of sequence encapsulates all animation duration too
      for (int h=0; h < _holders.Count; h++)
        _duration = math.max(_duration, _holders[h].EndTime);

      return _duration;
    }
  }
}