using UnityEngine.Playables;

namespace Voxell.MotionGFX
{
  public class MXTrackMixer : PlayableBehaviour
  {
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
      UnityEngine.Debug.Log("Mixer Process Frame");

      int inputCount = playable.GetInputCount();
      float trackTime = (float) playable.GetTime();

      for (int i=0; i < inputCount; i++)
      {
        ScriptPlayable<MXClipBehaviour> scriptPlayable = (ScriptPlayable<MXClipBehaviour>) playable.GetInput(i);
        MXClipBehaviour clipBehaviour = scriptPlayable.GetBehaviour();

        ISeqHolder seqHolder = clipBehaviour.scene;
        seqHolder.Evaluate(trackTime);
      }
    }
  }
}