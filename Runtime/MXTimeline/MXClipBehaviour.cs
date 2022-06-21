using UnityEngine.Playables;

#if UNITY_EDITOR
using UnityEditor.Timeline;
#endif

namespace Voxell.MotionGFX
{
  public class MXClipBehaviour : PlayableBehaviour
  {
    public MXScene scene;

    public void Init()
    {
      if (scene == null) return;

      #if UNITY_EDITOR
      if (TimelineEditor.inspectedDirector == null) return;
      float directorTime = (float) TimelineEditor.inspectedDirector.time;

      ISeqHolder seqHolder = scene;
      seqHolder.InitEvaluation(directorTime);
      #endif
    }
  }
}