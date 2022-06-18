namespace Voxell.MotionGFX
{
  internal static class MXUtil
  {
    internal static bool IsHolderInRange(IHolder holder, float localStartTime, float localEndTime)
    {
      float holderStartTime = holder.StartTime;
      float holderEndTime = holder.EndTime;

      return (
        // local start time in between holder start and end time
        (localStartTime >= holderStartTime && localStartTime <= holderEndTime) ||
        // local end time in between holder start and end time
        (localEndTime >= holderStartTime && localEndTime <= holderEndTime)
        // entire holder in between local start and end time
      ) || (localStartTime <= holderStartTime && localEndTime >= holderEndTime);
    }
  }
}