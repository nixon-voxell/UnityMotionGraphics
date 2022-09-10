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

        List<IHolder> ISeqHolder.Holders => m_Holders;
        private protected List<IHolder> m_Holders;

        public float StartTime =>
            ClipPlayable?.timelineClip == null ? 0.0f : (float) ClipPlayable.timelineClip.start;

        public float Duration => m_Duration;
        private protected float m_Duration;
        private float m_SavedDuration;

        float IHolder.EndTime => StartTime + m_Duration;

        float ISeqHolder.PrevGlobalTime { get; set; }
        float ISeqHolder.PrevStartTime { get; set; }
        float ISeqHolder.PrevDuration { get; set; }

        #region Unity Events

        private void OnValidate()
        {
            m_Holders = new List<IHolder>(m_Clips.Length);
            for (int c=0; c < m_Clips.Length; c++) m_Holders.Add(new MXSequence());

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
                ClipPlayable.timelineClip.duration = m_Duration;

                TimelineAsset timelineAsset = TimelineEditor.inspectedAsset;
                if (timelineAsset != null)
                {
                    // the minimum duration of a clip is the length of a single frame
                    double minDuration = 1.0d/timelineAsset.editorSettings.frameRate;
                    timelineClip.duration = math.max(minDuration, m_Duration);
                    MXClipPlayable ClipPlayable = timelineClip.asset as MXClipPlayable;
                }

                if (m_SavedDuration != m_Duration)
                {
                    OnDurationChange();
                    m_SavedDuration = m_Duration;
                }
            }
            #endif
        }

        private void CreateSequences()
        {
            m_Duration = 0.0f;

            for (int h=0; h < m_Holders.Count; h++)
            {
                if (m_Clips[h] == null) continue;

                MXSequence seq = m_Holders[h] as MXSequence;
                ISeqHolder seqHolder = seq as ISeqHolder;
                seqHolder.ClearHolders();

                m_Clips[h].CreateSequence(in seq);
                // accumulated duration will be the start time of the current sequence
                m_Duration += seq.CalculateDuration(m_Duration);
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