using UnityEngine.Playables;

namespace Voxell.MotionGFX
{
  public class MXClipBehaviour : PlayableBehaviour
  {
    public MXScene scene;

    // public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    // {
    //   if (scene == null) return;

    //   float playableTime = (float) playable.GetTime();
    //   ISeqHolder seqHolder = scene as ISeqHolder;
    //   seqHolder.Evaluate(playableTime);
    // }
  }
}