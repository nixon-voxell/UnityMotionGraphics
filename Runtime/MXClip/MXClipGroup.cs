using UnityEngine;
using UnityEngine.Timeline;
using Unity.Mathematics;

#if UNITY_EDITOR
using UnityEditor.Timeline;
#endif

namespace Voxell.MotionGFX
{
  using Inspector;

  [AddComponentMenu("Motion GFX/AbstractMXClip Group")]
  [ExecuteInEditMode]
  public class MXClipGroup : MonoBehaviour
  {
    [InspectOnly] public MXClipPlayable AbstractMXClip;

    private void OnEnable() => TimelineUpdate();

    private void Update()
    {
      TimelineUpdate();
    }

    private void TimelineUpdate()
    {
      #if UNITY_EDITOR
      if (AbstractMXClip != null)
      {
        TimelineClip timelineClip = AbstractMXClip.timelineClip;
        AbstractMXClip.timelineClip.duration = GetDuration();

        TrackAsset trackAsset = timelineClip.GetParentTrack();
        if (trackAsset != null)
        {
          // the minimum duration of a clip is the length of a single frame
          double minDuration = 1/trackAsset.timelineAsset.editorSettings.frameRate;
          timelineClip.duration = math.max(minDuration, GetDuration());
        }

        TimelineEditor.Refresh(RefreshReason.WindowNeedsRedraw);
      }
      #endif
    }

    public AbstractMXClip[] Clips => _clips;
    [SerializeField] private AbstractMXClip[] _clips;

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