using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace Voxell.MotionGraphics.Editor
{
    public class MotionTimelineWindow : EditorWindow
    {
        [SerializeField] private VisualTreeAsset m_VisualTreeAsset;
        [SerializeField] private Texture m_Icon;

        [MenuItem("Voxell/Motion GFX/Motion Graphics Timeline")]
        public static void CreateWindow()
        {
            MotionTimelineWindow window = GetWindow<MotionTimelineWindow>();
            window.titleContent = new GUIContent("Motion Graphics Timeline", window.m_Icon);
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
