using System.Collections.Generic;
using Unity.Mathematics;

namespace Voxell.MotionGFX
{
    public class MXSequence : ISeqHolder
    {
        List<IHolder> ISeqHolder.Holders => m_Holders;
        private protected List<IHolder> m_Holders;

        float IHolder.StartTime => m_StartTime;
        private protected float m_StartTime;

        float IHolder.Duration => m_Duration;
        private protected float m_Duration;

        float IHolder.EndTime => m_StartTime + m_Duration;

        float ISeqHolder.PrevGlobalTime { get; set; }
        float ISeqHolder.PrevStartTime { get; set; }
        float ISeqHolder.PrevDuration { get; set; }

        public MXSequence()
        {
            this.m_Holders = new List<IHolder>();
        }

        #region Sequence Creation

        /// <summary>Play an action.</summary>
        public MXActionHolder Play(MXAction action)
        {
            MXActionHolder actionHolder = new MXActionHolder(action);
            m_Holders.Add(actionHolder);

            return actionHolder;
        }

        /// <summary>Play multiple actions.</summary>
        public void PlaySeq(MXAction[] actions, float animateTime, float waitTime)
        {
            for (int a=0; a < actions.Length; a++)
            {
                MXActionHolder actionHolder = new MXActionHolder(actions[a]);
                actionHolder.Animate(animateTime).Wait(waitTime);
                m_Holders.Add(actionHolder);
            }
        }

        /// <summary>Play a one shot action in a single frame.</summary>
        public MXActionHolder OneShot(MXAction action)
        {
            MXActionHolder actionHolder = new MXActionHolder(action);
            actionHolder.Animate(0.0f).Wait(0.0f);
            m_Holders.Add(actionHolder);

            return actionHolder;
        }

        /// <summary>Play multiple shot actions in a single frame.</summary>
        public void OneShotSeq(MXAction[] actions)
        {
            for (int a=0; a < actions.Length; a++)
            {
                MXActionHolder actionHolder = new MXActionHolder(actions[a]);
                m_Holders.Add(actionHolder);
                actionHolder.Animate(0.0f).Wait(0.0f);
            }
        }

        /// <summary>Pause for a specific amount of duration.</summary>
        public void Pause(float duration) => Play(A_Internal.PauseAct).Animate(duration).Wait(duration);

        #endregion

        /// <returns>Total duration that the sequence uses.</returns>
        internal float CalculateDuration(float startTime)
        {
            m_StartTime = startTime;
            m_Duration = 0.0f;

            // setting up all start time is being assigned
            for (int h=0, holderCount=m_Holders.Count; h < holderCount; h++)
            {
                MXActionHolder actionHolder = m_Holders[h] as MXActionHolder;

                actionHolder.SetStartTime(m_Duration);
                m_Duration += actionHolder.WaitDuration;
            }

            // making sure that the duration of sequence encapsulates all animation duration too
            for (int h=0; h < m_Holders.Count; h++)
                m_Duration = math.max(m_Duration, m_Holders[h].EndTime);

            return m_Duration;
        }
    }
}