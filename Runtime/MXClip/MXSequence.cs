using System.Collections.Generic;
using Unity.Mathematics;

namespace Voxell.MotionGFX
{
  public class MXSequence
  {
    private List<AbstractMXAction> _actions;

    public float StartTime => _startTime;
    private float _startTime;

    public float SeqDuration => _seqDuration;
    private float _seqDuration;

    public float EndTime => _startTime + _seqDuration;

    public MXSequence()
    {
      this._actions = new List<AbstractMXAction>();
    }

    public AbstractMXAction PlayAction(in AbstractMXAction action)
    {
      _actions.Add(action);
      return action;
    }

    public void Pause(float duration)
    {
      AbstractMXAction pauseAction = new MXPauseAction(duration);
      PlayAction(in pauseAction);
    }

    /// <returns>Sum of wait duration of all actions</returns>
    public float CalculateDuration(float startTime)
    {
      _startTime = startTime;
      _seqDuration = 0.0f;

      // setting up all start time is being assigned
      for (int a=0; a < _actions.Count; a++)
      {
        _actions[a].SetStartTime(_seqDuration);
        _seqDuration += _actions[a].WaitDuration;
      }

      for (int a=0; a < _actions.Count; a++)
        _seqDuration = math.max(_seqDuration, _actions[a].EndTime);

      return _seqDuration;
    }

    public void Evaluate(float clipGroupTime)
    {
      float clipTime = clipGroupTime - _startTime;

      for (int a=0, actionCount=_actions.Count; a < actionCount; a++)
      {
        AbstractMXAction action = _actions[a];

        // evaluate if clip group time in between sequence start and end time
        if (clipTime >= action.StartTime && clipTime <= action.EndTime)
        {
          // actions can overlap, so we need to evaluate all occuring actions
          _actions[a].Evaluate(clipTime);
        }
      }
    }
  }
}