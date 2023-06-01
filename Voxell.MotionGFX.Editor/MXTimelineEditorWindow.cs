using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;

namespace Voxell.MotionGFX.Editor
{
    public class MXTimelineEditorWindow : EditorWindow
    {
        [SerializeField] private VisualTreeAsset m_VisualTreeAsset;
        [SerializeField] private Texture m_Icon;

        [MenuItem("Voxell/Motion GFX/MX Timeline")]
        public static void CreateWindow()
        {
            MXTimelineEditorWindow window = GetWindow<MXTimelineEditorWindow>();
            window.titleContent = new GUIContent("MX Timeline", window.m_Icon);
        }

        private VisualElement m_TimelineCanvas;

        private void CreateGUI()
        {
            this.m_VisualTreeAsset.CloneTree(this.rootVisualElement);
            this.minSize = new Vector2(800.0f, 400.0f);

            this.m_TimelineCanvas = this.rootVisualElement.Q<VisualElement>("timeline-canvas");
            this.m_TimelineCanvas.generateVisualContent += this.DrawTimelineCanvas;
        }

        private void DrawTimelineCanvas(MeshGenerationContext context)
        {
            // Painter2D painter = context.painter2D;
            // painter.strokeGradient
        }
    }
}
