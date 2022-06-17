using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Voxell.MotionGFX
{
  [TrackClipType(typeof(MXClipPlayable))]
  public class MXTrack : TrackAsset
  {
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
      return base.CreateTrackMixer(graph, go, inputCount);
    }
  }
}