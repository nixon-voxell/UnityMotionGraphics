using Unity.Mathematics;

namespace Voxell.MotionGFX
{
    public class MXActionHolder : IHolder
    {
        private MXAction m_Action;

        public float StartTime => m_StartTime;
        private protected float m_StartTime;

        public float Duration => m_Duration;
        private protected float m_Duration;

        public float EndTime => m_StartTime + m_Duration;

        public float WaitDuration => m_WaitDuration;
        private float m_WaitDuration;

        public MXActionHolder(MXAction action)
        {
            this.m_Action = action;
            this.m_StartTime = -1.0f;
            this.m_Duration = 1.0f;
            this.m_WaitDuration = 0.0f;
        }

        public MXActionHolder Animate(float duration)
        {
            m_Duration = math.max(duration, 0.0f);
            return this;
        }

        public MXActionHolder Wait(float duration = -1.0f)
        {
            if (duration == -1.0f) m_WaitDuration = m_Duration;
            else m_WaitDuration = math.max(duration, 0.0f);
            return this;
        }

        internal void SetStartTime(float startTime) => m_StartTime = startTime;

        public void Evaluate(float globalTime)
        {
            float t = CalculateTime(globalTime);
            m_Action(t);
        }

        /// <returns>Unit time value that is between the range of 0.0f and 1.0f</returns>
        private float CalculateTime(float globalTime)
        {
            // local time must be between action start and end time, so we clamp the time
            float localTime = math.clamp(
                globalTime, StartTime, EndTime
            ) - StartTime;

            // action time is evaulated with only unit values 0.0f -> 1.0f
            return localTime / Duration;
        }
    }
}