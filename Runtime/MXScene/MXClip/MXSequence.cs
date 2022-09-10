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
    float ISeqHolder.PrevStartTime { get; set; }
    float ISeqHolder.PrevDuration { get; set; }

    public MXSequence()
    {
      this._holders = new List<IHolder>();
    }

    #region Sequence Creation

    /// <summary>Play an action.</summary>
    public MXActionHolder Play(MXAction action)
    {
      MXActionHolder actionHolder = new MXActionHolder(action);
      _holders.Add(actionHolder);

      return actionHolder;
    }

    /// <summary>Play multiple actions.</summary>
    public void PlaySeq(MXAction[] actions, float animateTime, float waitTime)
    {
      for (int a=0; a < actions.Length; a++)
      {
        MXActionHolder actionHolder = new MXActionHolder(actions[a]);
        actionHolder.Animate(animateTime).Wait(waitTime);
        _holders.Add(actionHolder);
      }
    }

    /// <summary>Play a one shot action in a single frame.</summary>
    public MXActionHolder OneShot(MXAction action)
    {
      MXActionHolder actionHolder = new MXActionHolder(action);
      actionHolder.Animate(0.0f).Wait(0.0f);
      _holders.Add(actionHolder);

      return actionHolder;
    }

    /// <summary>Play multiple shot actions in a single frame.</summary>
    public void OneShotSeq(MXAction[] actions)
    {
      for (int a=0; a < actions.Length; a++)
      {
        MXActionHolder actionHolder = new MXActionHolder(actions[a]);
        _holders.Add(actionHolder);
        actionHolder.Animate(0.0f).Wait(0.0f);
      }
    }

    /// <summary>Pause for a specific amount of duration.</summary>
    public void Pause(float duration) => Play(A_Internal.PauseAct).Animate(duration).Wait(duration);

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