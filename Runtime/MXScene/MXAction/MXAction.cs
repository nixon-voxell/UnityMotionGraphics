using UnityEngine;
using Unity.Mathematics;

namespace Voxell.MotionGFX
{
  public static partial class MXAction
  {
    public delegate void Act(float t);

    internal static void PauseAct(float t) {}
  }
}