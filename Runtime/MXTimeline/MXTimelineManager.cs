using UnityEngine;
using UnityEngine.Playables;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Voxell.MotionGFX
{
    using Inspector;

    [AddComponentMenu("Motion GFX/MX Timeline Manager")]
    [ExecuteInEditMode]
    public class MXTimelineManager : MonoBehaviour
    {
        [InspectOnly, SerializeField] private PlayableDirector m_PlayableDirector;
        [InspectOnly, SerializeField] private double m_Time;

        private void Reset() => m_PlayableDirector = GetComponent<PlayableDirector>();

        #if UNITY_EDITOR
        private void OnEnable() => EditorApplication.delayCall += RebuildDirectorGraph;
        #endif

        [Button("Rebuild Playable Director Graph")]
        private void RebuildDirectorGraph()
        {
            if (Application.isPlaying || m_PlayableDirector == null) return;
            m_PlayableDirector.time = m_Time;
            m_PlayableDirector.RebuildGraph();
        }

        private void Update()
        {
            if (Application.isPlaying || m_PlayableDirector == null) return;
            m_Time = m_PlayableDirector.time; 
        }
    }
}