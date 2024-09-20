using System;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace UrbanFox.Editor
{
    [InitializeOnLoad]
    public static class ColorHierarchyDrawer
    {
        static ColorHierarchyDrawer()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnDrawHierarchyWindowItemOnGUI;
        }

        private static void OnDrawHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            var gameObject = (GameObject)EditorUtility.InstanceIDToObject(instanceID);
            if (gameObject != null)
            {
                var colorHierarchy = gameObject.GetComponent<ColorHierarchy>();
                if (colorHierarchy != null && Event.current.type == EventType.Repaint)
                {
                    var isObjectSelected = Selection.instanceIDs.Contains(instanceID);
                    var backgroundColor = colorHierarchy.BackgroundColor;
                    var textColor = colorHierarchy.TextColor;

                    if (!colorHierarchy.isActiveAndEnabled)
                    {
                        backgroundColor.a *= 0.5f;
                        textColor.a *= 0.5f;
                    }

                    var backgroundOffset = new Rect(selectionRect.position, selectionRect.size);
                    if (colorHierarchy.BackgroundColor.a < 1f || isObjectSelected)
                    {
                        EditorGUI.DrawRect(backgroundOffset, ColorHierarchy.DefaultBackgroundColor);
                    }
                    if (isObjectSelected)
                    {
                        EditorGUI.DrawRect(backgroundOffset, Color.Lerp(GUI.skin.settings.selectionColor, backgroundColor, 0.3f));
                    }
                    else
                    {
                        EditorGUI.DrawRect(backgroundOffset, backgroundColor);
                    }

                    var offset = new Rect(selectionRect.position + new Vector2(2, 0), selectionRect.size);
                    EditorGUI.LabelField(offset, gameObject.name, new GUIStyle()
                    {
                        normal = new GUIStyleState()
                        {
                            textColor = textColor
                        },
                        fontStyle = colorHierarchy.FontStyle
                    });
                    EditorApplication.RepaintHierarchyWindow();
                }
            }
        }
    }
}
