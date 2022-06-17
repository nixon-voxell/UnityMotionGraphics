using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using Unity.Mathematics;

#if UNITY_EDITOR
using UnityEditor.Timeline;
#endif

namespace Voxell.MotionGFX
{
  using Inspector;

  [AddComponentMenu("Motion GFX/MX Scene")]
  [ExecuteInEditMode]
  public class MXScene : MonoBehaviour
  {
    [InspectOnly] public MXClipPlayable clipPlayable;

    public MXSequence[] Sequences => _sequences;
    private MXSequence[] _sequences;

    public float Duration => _duration;
    private float _duration;
    private float __duration;

    public AbstractMXClip[] Clips => _clips;
    [SerializeField] private AbstractMXClip[] _clips;

    public void GenerateSequences()
    {
      _duration = 0.0f;
      _sequences = new MXSequence[_clips.Length];

      for (int c=0; c < _clips.Length; c++)
      {
        MXSequence seq = new MXSequence();

        _clips[c].CreateSequence(in seq);
        // accumulated duration will be the start time of the current sequence
        _duration += seq.CalculateDuration(_duration);

        _sequences[c] = seq;
      }
    }

    public void Evaluate(float sceneTime)
    {
      for (int s=0; s < _sequences.Length; s++)
      {
        MXSequence seq = _sequences[s];

        // evaluate if clip group time in between sequence start and end time
        if (sceneTime >= seq.StartTime && sceneTime <= seq.EndTime)
        {
          _sequences[s].Evaluate(sceneTime);

          // sequences cannot overlap, so we only ever need to evaluate one sequence at a time
          break;
        }
      }
    }

    private void OnEnable()
    {
      Debug.Log("OnEnable");
      GenerateSequences();
      TimelineUpdate();
    }

    private void Update()
    {
      GenerateSequences();
      TimelineUpdate();
    }

    private void TimelineUpdate()
    {
      #if UNITY_EDITOR
      if (clipPlayable != null)
      {
        TimelineClip timelineClip = clipPlayable.timelineClip;
        clipPlayable.timelineClip.duration = _duration;

        TrackAsset trackAsset = timelineClip.GetParentTrack();
        if (trackAsset != null)
        {
          // the minimum duration of a clip is the length of a single frame
          double minDuration = 1/trackAsset.timelineAsset.editorSettings.frameRate;
          timelineClip.duration = math.max(minDuration, _duration);
          // PlayableAsset playableAsset = timelineClip.asset as PlayableAsset;
          // Debug.Log(playableAsset);
        }

        TimelineEditor.Refresh(RefreshReason.WindowNeedsRedraw);

        if (__duration != _duration)
        {
          TimelineEditor.inspectedDirector.RebuildGraph();
          __duration = _duration;
        }
      }
      #endif
    }
  }
}