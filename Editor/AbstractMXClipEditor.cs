using UnityEngine;
using UnityEditor;

namespace Voxell.MotionGFX
{
  using Inspector;

  [CustomEditor(typeof(AbstractMXClip), true)]
  public class AbstractMXClipEditor : VXDefaultEditor
  {
    public override void OnInspectorGUI()
    {
      GUI.enabled = !EditorApplication.isPlaying;
      base.OnInspectorGUI();
      GUI.enabled = true;
    }
  }
}