using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using Unity.Mathematics;

#if UNITY_EDITOR
using UnityEditor.Timeline;
#endif

namespace Voxell.MotionGFX
{
  using Inspector;

  [AddComponentMenu("Motion GFX/MX Scene")]
  [ExecuteInEditMode]
  public class MXScene : MonoBehaviour, ISeqHolder
  {
    [InspectOnly] public MXClipPlayable clipPlayable;

    public AbstractMXClip[] Clips => _clips;
    [SerializeField] private AbstractMXClip[] _clips;

    List<IHolder> ISeqHolder.Holders => _holders;
    private protected List<IHolder> _holders;

    float IHolder.StartTime => _startTime;
    private protected float _startTime;

    float IHolder.Duration => _duration;
    private protected float _duration;
    private float __duration;

    float IHolder.EndTime => _startTime + _duration;

    float ISeqHolder.PrevGlobalTime { get; set; }

    #region Unity Events

    private void OnValidate()
    {
      _holders = new List<IHolder>(_clips.Length);
      for (int c=0; c < _clips.Length; c++) _holders.Add(new MXSequence());
      TimelineClipUpdate();
    }

    private void Update()
    {
      TimelineClipUpdate();
    }

    #endregion

    private void TimelineClipUpdate()
    {
      _duration = 0.0f;

      for (int h=0; h < _holders.Count; h++)
      {
        MXSequence seq = _holders[h] as MXSequence;
        ISeqHolder seqHolder = seq as ISeqHolder;
        seqHolder.ClearHolders();

        _clips[h].CreateSequence(in seq);
        // accumulated duration will be the start time of the current sequence
        _duration += seq.CalculateDuration(_duration);
      }

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
        }

        if (__duration != _duration)
        {
          OnDurationChange();
          __duration = _duration;
        }
      }
      #endif
    }

    /// <summary>Redraw timeline window and rebuild director grpah.</summary>
    /// <remarks>The director graph needs to be rebuilt in order to cater for the change in clip length</remarks>
    private void OnDurationChange()
    {
      #if UNITY_EDITOR
      if (TimelineEditor.inspectedDirector != null) TimelineEditor.inspectedDirector.RebuildGraph();
      TimelineEditor.Refresh(RefreshReason.WindowNeedsRedraw);
      #endif
    }
  }
}