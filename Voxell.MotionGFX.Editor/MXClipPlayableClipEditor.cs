using UnityEngine.Timeline;
using UnityEditor.Timeline;

namespace Voxell.MotionGFX
{
  [CustomTimelineEditor(typeof(MXClipPlayable))]
  public class MXClipPlayableClipEditor : ClipEditor
  {
    public override void OnCreate(TimelineClip clip, TrackAsset track, TimelineClip clonedFrom)
    {
      clip.displayName = "MX Clip";
    }

    public override void OnClipChanged(TimelineClip clip)
    {
      // assign timeline clip to the clip asset
      MXClipPlayable clipAsset = clip.asset as MXClipPlayable;
      clipAsset.timelineClip = clip;
    }
  }
}