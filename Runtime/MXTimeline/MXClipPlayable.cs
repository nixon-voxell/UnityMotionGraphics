using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Voxell.MotionGFX
{
  public class MXClipPlayable : PlayableAsset
  {
    [HideInInspector] public TimelineClip timelineClip;
    public ExposedReference<MXScene> sceneRef;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
      MXScene scene = sceneRef.Resolve(graph.GetResolver());
      if (scene != null) scene.clipPlayable = this;
      MXClipBehaviour clipBehaviour = new MXClipBehaviour() { scene = scene };

      ScriptPlayable<MXClipBehaviour> playable = ScriptPlayable<MXClipBehaviour>.Create(graph, clipBehaviour);
      return playable;
    }
  }
}