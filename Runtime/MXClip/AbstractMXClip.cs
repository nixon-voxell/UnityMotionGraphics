using UnityEngine;

namespace Voxell.MotionGFX
{
  public abstract class AbstractMXClip : MonoBehaviour
  {
    public abstract void CreateSequence(in MXSequence s);
  }
}