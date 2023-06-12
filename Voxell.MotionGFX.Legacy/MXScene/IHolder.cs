namespace Voxell.MotionGFX
{
    internal interface IHolder
    {
        float StartTime { get; }
        float Duration { get; }
        float EndTime { get; }

        void InitEvaluation(float globalTime) => Evaluate(globalTime);
        void Evaluate(float globalTime);
    }
}