using UnityEngine;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine.Timeline;
using UnityEngine.Playables;

namespace Voxell.MotionGFX
{
  public class MXTimelineWindow : TimelineEditorWindow
  {
    [MenuItem("Tools/Voxell/MX Timeline Window")]
    public static void ShowWindow()
    {
      EditorWindow window = GetWindow(typeof(MXTimelineWindow));
      window.titleContent = new GUIContent("MX Timeline Window");
    }

    public override TimelineNavigator navigator => throw new System.NotImplementedException();

    public override TimelinePlaybackControls playbackControls => throw new System.NotImplementedException();

    public override bool locked { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public override void ClearTimeline()
    {
      throw new System.NotImplementedException();
    }

    public override void SetTimeline(TimelineAsset sequence)
    {
      throw new System.NotImplementedException();
    }

    public override void SetTimeline(PlayableDirector director)
    {
      throw new System.NotImplementedException();
    }
  }
}