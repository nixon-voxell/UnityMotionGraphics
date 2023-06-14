namespace Voxell.MotionGraphics.Storage
{
    using Util.Interface;

    public interface IConfig<T> : IValidate, IDefault<T>
    where T : unmanaged
    {
    }
}
