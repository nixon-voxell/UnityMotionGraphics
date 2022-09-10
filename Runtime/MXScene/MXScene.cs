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
    [InspectOnly] public MXClipPlayable ClipPlayable;

    public AbstractMXClip[] Clips => m_Clips;
    [SerializeField] private AbstractMXClip[] m_Clips;

    List<IHolder> ISeqHolder.Holders => _holders;
    private protected List<IHolder> _holders;

    public float StartTime =>
      ClipPlayable?.timelineClip == null ? 0.0f : (float) ClipPlayable.timelineClip.start;

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
      _holders = new List<IHolder>(m_Clips.Length);
      for (int c=0; c < m_Clips.Length; c++) _holders.Add(new MXSequence());

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
      if (ClipPlayable != null)
      {
        TimelineClip timelineClip = ClipPlayable.timelineClip;
        ClipPlayable.timelineClip.duration = _duration;

        TimelineAsset timelineAsset = TimelineEditor.inspectedAsset;
        if (timelineAsset != null)
        {
          // the minimum duration of a clip is the length of a single frame
          double minDuration = 1.0d/timelineAsset.editorSettings.frameRate;
          timelineClip.duration = math.max(minDuration, _duration);
          MXClipPlayable ClipPlayable = timelineClip.asset as MXClipPlayable;
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
        if (m_Clips[h] == null) continue;

        MXSequence seq = _holders[h] as MXSequence;
        ISeqHolder seqHolder = seq as ISeqHolder;
        seqHolder.ClearHolders();

        m_Clips[h].CreateSequence(in seq);
        // accumulated duration will be the start time of the current sequence
        _duration += seq.CalculateDuration(_duration);
      }
    }

    internal void Init()
    {
      for (int c=0; c < m_Clips.Length; c++)
      {
        if (m_Clips[c] == null) continue;
        if (!m_Clips[c].Initialized) m_Clips[c].Init();
      }
    }

    internal void CleanUp()
    {
      for (int c=0; c < m_Clips.Length; c++)
      {
        if (m_Clips[c] == null) continue;
        if (m_Clips[c].Initialized) m_Clips[c].CleanUp();
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