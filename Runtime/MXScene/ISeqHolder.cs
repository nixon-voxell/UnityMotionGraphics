using System.Collections.Generic;
using Unity.Mathematics;

namespace Voxell.MotionGFX
{
  internal interface ISeqHolder : IHolder
  {
    /// <summary>Time used to extend the start and end range.</summary>
    const float BUFFER_TIME = 0.1f;

    List<IHolder> Holders { get; }
    float PrevGlobalTime { get; internal protected set; }
    float PrevStartTime { get; internal protected set; }
    float PrevDuration { get; internal protected set; }

    void ClearHolders() => Holders.Clear();

    void InitEvaluation(float globalTime)
    {
      UpdatePreviousData(globalTime);
      float localTime = globalTime - StartTime;

      int holderIdx = 0;
      for (int h=0, holderCount=Holders.Count; h < holderCount; h++)
      {
        // we leave the holder that is currently in the range of local time to be the last to be evalulated
        if (MXUtil.IsHolderInRange(Holders[h], localTime, localTime)) holderIdx = h;
      }

      // evaluate from last holder to holder idx
      for (int h=Holders.Count; h-- > holderIdx;) Holders[h].Evaluate(localTime);
      // then evaluate from first holder to holder idx
      for (int h=0; h < holderIdx; h++) Holders[h].Evaluate(localTime);

      // lastly we evaluate the clip that is currently in the range of clip time
      Holders[holderIdx].Evaluate(localTime);
    }

    void IHolder.Evaluate(float globalTime)
    {
      float durationChange = Duration - PrevDuration;
      float localTime = globalTime - StartTime;
      float prevLocalTime = PrevGlobalTime - PrevStartTime + durationChange;

      // direction that the time moves
      float timeDirection = math.sign(localTime - prevLocalTime);
      if (timeDirection == 0.0f) return;

      float localStartTime = math.min(localTime, prevLocalTime) - BUFFER_TIME;
      float localEndTime = math.max(localTime, prevLocalTime) + BUFFER_TIME;

      int holderIdx = 0;
      int holderLength = 0;
      for (int h=0, holderCount=Holders.Count; h < holderCount; h++)
      {
        // actions can overlap, so we need to evaluate all occuring actions
        if (MXUtil.IsHolderInRange(Holders[h], localStartTime, localEndTime))
        {
          holderIdx = h;
          holderLength++;
        }
      }

      // time moving forward
      if (timeDirection == 1.0f)
      {
        // evaluate forward from start to end
        int startIdx = holderIdx - (holderLength - 1);
        int endIdx = startIdx + holderLength;

        for (int h=startIdx; h < endIdx; h++) Holders[h].Evaluate(localTime);
      } else // time moving backward
      {
        // evaluate backward from end to start
        int startIdx = holderIdx - (holderLength - 1);
        int endIdx = holderIdx + 1;

        for (int h=endIdx; h-- > startIdx;) Holders[h].Evaluate(localTime);
      }

      UpdatePreviousData(globalTime);
    }

    private void UpdatePreviousData(float globalTime)
    {
      PrevGlobalTime = globalTime;
      PrevStartTime = StartTime;
      PrevDuration = Duration;
    }
  }
}