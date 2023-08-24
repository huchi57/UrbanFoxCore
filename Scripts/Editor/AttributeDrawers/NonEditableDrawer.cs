using UnityEngine;
using UnityEditor;

namespace UrbanFox.Editor
{
    [CustomPropertyDrawer(typeof(NonEditableAttribute), true)]
    public class NonEditableDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var cacheGUIState = GUI.enabled;
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = cacheGUIState;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
}
