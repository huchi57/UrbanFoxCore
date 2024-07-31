using UnityEngine;
using UnityEditor;

namespace UrbanFox.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UILocalizedText))]
    public class UILocalizedTextEditor : UnityEditor.Editor
    {
        private UILocalizedText m_target;

        private void OnEnable()
        {
            m_target = (UILocalizedText)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();
            if (GUILayout.Button("Localize Text Now"))
            {
                m_target.LocalizeContent();
            }
        }
    }
}
