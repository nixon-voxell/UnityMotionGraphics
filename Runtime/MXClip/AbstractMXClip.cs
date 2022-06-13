using UnityEngine;

namespace Voxell.MotionGFX
{
  public abstract class AbstractMXClip : MonoBehaviour
  {
    public abstract AbstractMXSequence CreateSequence();

    public float GetDuration()
    {
      AbstractMXSequence sequence = CreateSequence();

      float duration = 0.0f;
      while (sequence != null)
      {
        duration += sequence.CalculateDuration();
        sequence = sequence.NextSequence;
      }

      return duration;
    }
  }
}