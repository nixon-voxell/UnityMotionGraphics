using UnityEngine;
using Unity.Mathematics;

namespace Voxell.MotionGFX
{
  public class MXTransformAction : AbstractMXAction
  {
    private Data data;

    [System.Serializable]
    public struct Data
    {
      public Transform targetT;
      public Transform startT;
      public Transform endT;
    }

    public MXTransformAction(float animDuration, ref Data data, MXMath.Transition transition) : base(animDuration)
    {
      this.data = data;
      this.transition = transition;
    }

    public override void Evaluate(float clipTime)
    {
      if (data.targetT == null || data.startT == null || data.endT == null) return;

      float seqTime = clipTime - StartTime;
      float t = seqTime / _animDuration;

      // transition from start transform to end transform
      data.targetT.position = math.lerp(data.startT.position, data.endT.position, transition(t));
      data.targetT.rotation = math.slerp(data.startT.rotation, data.endT.rotation, transition(t));
      data.targetT.localScale = math.lerp(data.startT.localScale, data.endT.localScale, transition(t));
    }
  }
}