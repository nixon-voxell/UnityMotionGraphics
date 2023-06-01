using UnityEngine;
using UnityEditor;

namespace Voxell.MotionGFX.Editor
{
    [CustomEditor(typeof(AbstractMXClip), true)]
    public class AbstractMXClipEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            GUI.enabled = !EditorApplication.isPlaying;
            base.OnInspectorGUI();
            GUI.enabled = true;
        }
    }
}
