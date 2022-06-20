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
    [InspectOnly, SerializeField] private PlayableDirector _playableDirector;
    [InspectOnly, SerializeField] private double _time;

    private void Reset() => _playableDirector = GetComponent<PlayableDirector>();

    #if UNITY_EDITOR
    private void OnEnable() => EditorApplication.delayCall += RebuildDirectorGraph;
    #endif

    [Button("Rebuild Playable Director Graph")]
    private void RebuildDirectorGraph()
    {
      if (Application.isPlaying || _playableDirector == null) return;
      _playableDirector.time = _time;
      _playableDirector.RebuildGraph();
    }

    private void Update()
    {
      if (Application.isPlaying || _playableDirector == null) return;
      _time = _playableDirector.time; 
    }
  }
}