using UnityEngine;

namespace Voxell.MotionGraphics
{
    using Storage;

    public class MotionManagerAuthoring : MonoBehaviour
    {
        [SerializeField] private SO_MotionConfig m_so_MotionConfig;

        public SO_MotionConfig so_MotionConfig => this.m_so_MotionConfig;
    }
}
