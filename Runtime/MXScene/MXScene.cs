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

    public float StartTime =>
      clipPlayable?.timelineClip == null ? 0.0f : (float) clipPlayable.timelineClip.start;

    public float Duration => _duration;
    private protected float _duration;
    private float __duration;

    float IHolder.EndTime => StartTime + _duration;

    float ISeqHolder.PrevGlobalTime { get; set; }
    float ISeqHolder.PrevStartTime { get; set; }
    float ISeqHolder.PrevDuration { get; set; }

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

    internal void TimelineClipUpdate()
    {
      CreateSequences();

      #if UNITY_EDITOR
      if (clipPlayable != null)
      {
        TimelineClip timelineClip = clipPlayable.timelineClip;
        clipPlayable.timelineClip.duration = _duration;

        TimelineAsset timelineAsset = TimelineEditor.inspectedAsset;
        if (timelineAsset != null)
        {
          // the minimum duration of a clip is the length of a single frame
          double minDuration = 1.0d/timelineAsset.editorSettings.frameRate;
          timelineClip.duration = math.max(minDuration, _duration);
          MXClipPlayable clipPlayable = timelineClip.asset as MXClipPlayable;
        }

        if (__duration != _duration)
        {
          OnDurationChange();
          __duration = _duration;
        }
      }
      #endif
    }

    private void CreateSequences()
    {
      _duration = 0.0f;

      for (int h=0; h < _holders.Count; h++)
      {
        if (_clips[h] == null) continue;

        MXSequence seq = _holders[h] as MXSequence;
        ISeqHolder seqHolder = seq as ISeqHolder;
        seqHolder.ClearHolders();

        _clips[h].CreateSequence(in seq);
        // accumulated duration will be the start time of the current sequence
        _duration += seq.CalculateDuration(_duration);
      }
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