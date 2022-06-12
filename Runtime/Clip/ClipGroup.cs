using UnityEngine;
using UnityEngine.Timeline;
using Unity.Mathematics;

#if UNITY_EDITOR
using UnityEditor.Timeline;
#endif

namespace Voxell.MotionGFX
{
  using Inspector;

  [ExecuteInEditMode]
  public class ClipGroup : MonoBehaviour
  {
    [InspectOnly] public MXClipPlayable MXClip;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
      if (MXClip != null)
      {
        TimelineClip timelineClip = MXClip.timelineClip;
        MXClip.timelineClip.duration = GetDuration();

        TrackAsset trackAsset = timelineClip.GetParentTrack();
        if (trackAsset != null)
        {
          // the minimum duration of a clip is the length of a single frame
          double minDuration = 1/trackAsset.timelineAsset.editorSettings.frameRate;
          timelineClip.duration = math.max(minDuration, GetDuration());
        }

        #if UNITY_EDITOR
        TimelineEditor.Refresh(RefreshReason.WindowNeedsRedraw);
        #endif
      }
    }

    public Clip[] Clips => _clips;
    [SerializeField] private Clip[] _clips;

    /// <returns>Sum of the duration of all clips.</returns>
    public float GetDuration()
    {
      float duration = 0.0f;

      for (int c=0; c < _clips.Length; c++)
        duration += _clips[c] != null ? _clips[c].GetDuration() : 0.0f;

      return duration;
    }
  }
}