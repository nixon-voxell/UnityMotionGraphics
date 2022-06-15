using System.Collections.Generic;
using Unity.Mathematics;

namespace Voxell.MotionGFX
{
  public class MXSequence
  {
    public float StartTime => _startTime;
    private float _startTime;

    public float SeqDuration => _seqDuration;
    private float _seqDuration;

    public float EndTime => _startTime + _seqDuration;

    private List<MXActionHolder> _actionHolders;

    public MXSequence()
    {
      this._actionHolders = new List<MXActionHolder>();
    }

    public MXActionHolder Play(MXAction.Act action)
    {
      MXActionHolder actionHolder = new MXActionHolder(action);
      _actionHolders.Add(actionHolder);

      return actionHolder;
    }

    public void Pause(float duration) => Play(MXAction.PauseAct).Wait(duration);

    /// <returns>Sum of wait duration of all actions</returns>
    public float CalculateDuration(float startTime)
    {
      _startTime = startTime;
      _seqDuration = 0.0f;

      // setting up all start time is being assigned
      for (int a=0; a < _actionHolders.Count; a++)
      {
        MXActionHolder actionHolder = _actionHolders[a];

        actionHolder.SetStartTime(_seqDuration);
        _seqDuration += actionHolder.WaitDuration;
      }

      // making sure that the duration of sequence encapsulates all animation duration too
      for (int a=0; a < _actionHolders.Count; a++)
        _seqDuration = math.max(_seqDuration, _actionHolders[a].EndTime);

      return _seqDuration;
    }

    public void Evaluate(float sceneTime)
    {
      float clipTime = sceneTime - _startTime;

      for (int a=0, actionCount=_actionHolders.Count; a < actionCount; a++)
      {
        MXActionHolder actionHolder = _actionHolders[a];

        float actionTime = math.clamp(
          clipTime, actionHolder.StartTime, actionHolder.EndTime
        ) - actionHolder.StartTime;

        float t = actionTime / actionHolder.AnimDuration;

        // actions can overlap, so we need to evaluate all occuring actions
        actionHolder.action(t);
      }
    }
  }
}