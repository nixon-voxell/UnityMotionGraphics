using UnityEngine.Playables;

#if UNITY_EDITOR
using UnityEditor.Timeline;
#endif

namespace Voxell.MotionGFX
{
  public class MXClipBehaviour : PlayableBehaviour
  {
    public MXScene scene;

    public override void OnPlayableCreate(Playable playable)
    {
      if (scene == null) return;
      scene.Init();

      #if UNITY_EDITOR
      if (TimelineEditor.inspectedDirector == null) return;
      float directorTime = (float) TimelineEditor.inspectedDirector.time;

      ISeqHolder seqHolder = scene;
      seqHolder.InitEvaluation(directorTime);
      #endif
    }

    public override void OnPlayableDestroy(Playable playable)
    {
      if (scene == null) return;
      scene.CleanUp();
    }
  }
}