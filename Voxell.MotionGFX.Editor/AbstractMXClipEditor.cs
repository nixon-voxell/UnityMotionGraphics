using UnityEngine;
using UnityEditor;

namespace Voxell.MotionGFX
{
    [CustomEditor(typeof(AbstractMXClip), true)]
    public class AbstractMXClipEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GUI.enabled = !EditorApplication.isPlaying;
            base.OnInspectorGUI();
            GUI.enabled = true;
        }
    }
}
