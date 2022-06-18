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
      ScriptPlayable<MXClipBehaviour> playable = ScriptPlayable<MXClipBehaviour>.Create(graph);
      MXClipBehaviour clipBehaviour = playable.GetBehaviour();

      MXScene scene = sceneRef.Resolve(graph.GetResolver());
      // assign playable asset to clip group component so that it can manually refresh
      clipBehaviour.scene = scene;
      if (scene != null)
      {
        scene.clipPlayable = this;
      }

      return playable;
    }
  }
}