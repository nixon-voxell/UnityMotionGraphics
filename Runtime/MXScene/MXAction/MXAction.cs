using UnityEngine;
using Unity.Mathematics;

namespace Voxell.MotionGFX
{
  public static partial class MXAction
  {
    public delegate void Act(float t);

    public static void PauseAct(float t) {}
  }
}