using UnityEngine;

namespace Voxell.MotionGFX
{
    [ExecuteInEditMode]
    public abstract class AbstractMXClip : MonoBehaviour
    {
        public bool Initialized { get; private set; } = false;

        public virtual void Init() => Initialized = true;

        public abstract void CreateSequence(in MXSequence s);

        public void MXDestroy(Object obj)
        {
            if (Application.IsPlaying(obj)) Destroy(obj);
            else DestroyImmediate(obj);
        }

        public virtual void CleanUp() => Initialized = false;
    }
}