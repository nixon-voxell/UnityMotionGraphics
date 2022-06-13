using UnityEngine;

namespace Voxell.MotionGFX
{
  public abstract class AbstractMXSequence
  {
    public AbstractMXSequence NextSequence => _nextSequence;
    private protected AbstractMXSequence _nextSequence;

    public void Chain(in AbstractMXSequence sequence) => _nextSequence = sequence;

    public abstract float CalculateDuration();
  }
}