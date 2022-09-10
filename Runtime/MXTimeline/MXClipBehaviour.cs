using UnityEngine.Playables;

#if UNITY_EDITOR
using UnityEditor.Timeline;
#endif

namespace Voxell.MotionGFX
{
  public class MXClipBehaviour : PlayableBehaviour
  {
    public MXScene Scene;

    public override void OnPlayableCreate(Playable playable)
    {
      if (Scene == null) return;
      Scene.Init();

      #if UNITY_EDITOR
      if (TimelineEditor.inspectedDirector == null) return;
      float directorTime = (float) TimelineEditor.inspectedDirector.time;

      ISeqHolder seqHolder = Scene;
      seqHolder.InitEvaluation(directorTime);
      #endif
    }

    public override void OnPlayableDestroy(Playable playable)
    {
      if (Scene == null) return;
      Scene.CleanUp();
    }
  }
}