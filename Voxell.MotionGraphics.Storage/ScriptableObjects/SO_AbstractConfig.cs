using UnityEngine;

namespace Voxell.MotionGraphics.Storage
{
    public abstract class AbstractConfig_SO<T> : ScriptableObject
    where T : unmanaged, IConfig<T>
    {
        public T Data = new T().Default();

        public void OnValidate()
        {
            this.Data.Validate();
        }
    }
}
