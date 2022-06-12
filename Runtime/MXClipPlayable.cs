using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Voxell.MotionGFX
{
  public class MXClipPlayable : PlayableAsset
  {
    [HideInInspector] public TimelineClip timelineClip;
    public ExposedReference<ClipGroup> clipGroupRef;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
      ScriptPlayable<MXClipBehaviour> playable = ScriptPlayable<MXClipBehaviour>.Create(graph);
      MXClipBehaviour clipBehaviour = playable.GetBehaviour();

      ClipGroup clipGroup = clipGroupRef.Resolve(graph.GetResolver());
      // assign playable asset to clip group component so that it can manually refresh
      if (clipGroup != null) clipGroup.MXClip = this;
      clipBehaviour.clipGroup = clipGroup;

      return playable;
    }
  }
}