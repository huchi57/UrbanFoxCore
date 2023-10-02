using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace UrbanFox.Editor
{
    public class HierarchyIconSettings : EditorWindow
    {
        [Serializable]
        public struct EditorData
        {
            public string SearchText;
            public bool TypesWithIconOnly;
            public bool EnalbedTypesOnly;
        }

        private const int m_rightColumnWidth = 60;

        private EditorData m_editorData;
        private Vector2 m_scroll;
        private int m_numberOfFoundTypes = 0;
        private GUIStyle m_alignRightStyle;

        private static string EditorPrefsKey => $"{Application.companyName}/{Application.productName}/{typeof(HierarchyIconSettings)}";

        [MenuItem("OwO/Window/Hierarchy Icon Settings...")]
        private static void ShowWindow()
        {
            var window = GetWindow<HierarchyIconSettings>();
            window.titleContent = new GUIContent("Hierarchy Icon Settings");
            window.Show();
        }

        private void OnEnable()
        {
            m_editorData = EditorPrefs.HasKey(EditorPrefsKey) ? JsonUtility.FromJson<EditorData>(EditorPrefs.GetString(EditorPrefsKey)) : new EditorData()
            {
                SearchText = string.Empty,
                TypesWithIconOnly = true,
                EnalbedTypesOnly = false
            };
        }

        private void OnDisable()
        {
            EditorPrefs.SetString(EditorPrefsKey, JsonUtility.ToJson(m_editorData));
        }

        private void OnGUI()
        {
            if (DrawIconInHierarchy.AllComponentTypes.IsNullOrEmpty())
            {
                return;
            }

            if (m_alignRightStyle == null)
            {
                m_alignRightStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    alignment = TextAnchor.MiddleRight
                };
            }

            m_editorData.SearchText = EditorGUILayoutExtensions.SearchText("Search", m_editorData.SearchText);
            m_editorData.TypesWithIconOnly = EditorGUILayout.Toggle("Types With Icon Only", m_editorData.TypesWithIconOnly);
            m_editorData.EnalbedTypesOnly = EditorGUILayout.Toggle("Enabled Types Only", m_editorData.EnalbedTypesOnly);
            EditorGUILayout.HelpBox("Script icons of the enable types below will be drawn in the Hierarchy.", MessageType.Info);
            EditorGUILayout.Space();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"Found Component Types ({m_numberOfFoundTypes})", EditorStyles.boldLabel, GUILayout.Width(position.width - m_rightColumnWidth - 20));
            GUILayout.Label("Enable", m_alignRightStyle, GUILayout.Width(m_rightColumnWidth - 10));
            GUILayout.EndHorizontal();

            GUILayoutExtensions.HorizontalLine();
            m_numberOfFoundTypes = 0;
            m_scroll = GUILayout.BeginScrollView(m_scroll);
            var cachedSearchText = m_editorData.SearchText.ToLower();
            foreach (var type in DrawIconInHierarchy.AllComponentTypes)
            {
                if (type.Name.ToLower().Contains(cachedSearchText))
                {
                    if (!m_editorData.TypesWithIconOnly || DrawIconInHierarchy.GetComponentIcon(type) != null)
                    {
                        if (!m_editorData.EnalbedTypesOnly || DrawIconInHierarchy.IsTypeDrawnInHierarchy(type))
                        {
                            m_numberOfFoundTypes++;
                            GUILayout.BeginHorizontal();
                            GUILayout.Label(DrawIconInHierarchy.GetComponentIcon(type), GUILayout.Width(DrawIconInHierarchy.IconSize), GUILayout.Height(EditorGUIUtility.singleLineHeight));
                            GUILayout.Label(new GUIContent(type.Name, type.FullName), GUILayout.Width(position.width - DrawIconInHierarchy.IconSize - m_rightColumnWidth - 20));
                            if (DrawIconInHierarchy.IsTypeDrawnInHierarchy(type))
                            {
                                if (GUILayoutExtensions.ColoredButton("On", Color.green, GUILayout.Width(m_rightColumnWidth - 10)))
                                {
                                    DrawIconInHierarchy.RemoveType(type);
                                }
                            }
                            else
                            {
                                if (GUILayoutExtensions.ColoredButton("Off", Color.red, GUILayout.Width(m_rightColumnWidth - 10)))
                                {
                                    DrawIconInHierarchy.AddType(type);
                                }
                            }
                            GUILayout.EndHorizontal();
                        }
                    }
                }
            }
            if (m_numberOfFoundTypes <= 0)
            {
                EditorGUILayout.HelpBox("No Component types can be found.", MessageType.Info);
            }
            GUILayout.EndScrollView();
        }
    }

    [InitializeOnLoad]
    public static class DrawIconInHierarchy
    {
        public const int IconSize = 16;
        public static readonly List<Type> AllComponentTypes = new List<Type>();

        private static readonly Dictionary<Type, Texture2D> m_typesAndIcon = new Dictionary<Type, Texture2D>();
        private static readonly List<Type> m_drawIconTypes;

        private static string EditorPrefsKey => $"{Application.companyName}/{Application.productName}/{nameof(m_drawIconTypes)}";

        static DrawIconInHierarchy()
        {
            EditorApplication.hierarchyWindowItemOnGUI += DrawIconOnWindowItem;
            m_drawIconTypes = EditorPrefs.HasKey(EditorPrefsKey) ? JsonUtility.FromJson<List<Type>>(EditorPrefs.GetString(EditorPrefsKey)) : new List<Type>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var newTypes = assembly.GetTypes().Where(type => type.IsClass
                    && type.IsSubclassOf(typeof(Component))
                    && !type.IsAbstract
                    && !type.IsGenericType);
                AllComponentTypes.AddRange(newTypes);
            }
            TryFindIconsFromType(AllComponentTypes);
        }

        public static bool IsTypeDrawnInHierarchy(Type type)
        {
            return m_drawIconTypes.Contains(type);
        }

        public static void AddType(Type type)
        {
            if (!m_drawIconTypes.Contains(type))
            {
                m_drawIconTypes.Add(type);
                SaveSettings();
            }
        }

        public static void RemoveType(Type type)
        {
            if (m_drawIconTypes.Contains(type))
            {
                m_drawIconTypes.Remove(type);
                SaveSettings();
            }
        }

        public static Texture2D GetComponentIcon(Type type)
        {
            if (m_typesAndIcon.ContainsKey(type))
            {
                return m_typesAndIcon[type];
            }
            return null;
        }

        public static Texture2D GetComponentIcon(UnityEngine.Object target)
        {
            if (target == null)
            {
                return null;
            }

            string pathOfComponentScript;
            if (target is MonoBehaviour)
            {
                pathOfComponentScript = AssetDatabase.GetAssetPath(MonoScript.FromMonoBehaviour((MonoBehaviour)target));
            }
            else if (target is ScriptableObject)
            {
                pathOfComponentScript = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject((ScriptableObject)target));
            }
            else
            {
                return null;
            }

            var scriptImporter = (MonoImporter)AssetImporter.GetAtPath(pathOfComponentScript);
            if (scriptImporter)
            {
                return scriptImporter.GetIcon();
            }

            return null;
        }

        private static void TryFindIconsFromType(List<Type> types)
        {
            if (types.IsNullOrEmpty())
            {
                return;
            }

            // TODO: Not a very good way because it assumes the type matches the name of the script, and it cannot find built-in types as well
            foreach (var type in types)
            {
                // Ignore duplicates
                if (!m_typesAndIcon.ContainsKey(type))
                {
                    var scriptGUID = AssetDatabase.FindAssets(type.Name).FirstOrDefault();

                    // In case a GUID cannot be found
                    if (!string.IsNullOrEmpty(scriptGUID))
                    {
                        var scriptImporter = AssetImporter.GetAtPath(AssetDatabase.GUIDToAssetPath(scriptGUID));

                        // In case the importer cannot be cast into a MonoImporter
                        if (scriptImporter is MonoImporter monoImporter)
                        {
                            m_typesAndIcon.Add(type, monoImporter.GetIcon());
                        }
                    }
                }
            }
        }

        private static void DrawIconOnWindowItem(int instanceID, Rect selectionRect)
        {
            var gameObject = (GameObject)EditorUtility.InstanceIDToObject(instanceID);
            if (gameObject && !m_drawIconTypes.IsNullOrEmpty())
            {
                int iconCount = 1;
                foreach (var type in m_drawIconTypes)
                {
                    if (gameObject.TryGetComponent(type, out var component))
                    {
                        // Add the new type to dictionary if a key does not exist
                        if (!m_typesAndIcon.ContainsKey(type))
                        {
                            m_typesAndIcon.Add(type, GetComponentIcon(component));
                        }

                        // Re-fetch component icon if a key is found but the corresponding texture is lost
                        else if (m_typesAndIcon[type] == null)
                        {
                            m_typesAndIcon[type] = GetComponentIcon(component);
                        }

                        var iconRect = new Rect(selectionRect)
                        {
                            xMin = selectionRect.xMax - IconSize * iconCount,
                            height = IconSize
                        };
                        GUI.Label(iconRect, m_typesAndIcon[type]);
                        iconCount++;
                    }
                }
            }
        }

        private static void SaveSettings()
        {
            EditorPrefs.SetString(EditorPrefsKey, JsonUtility.ToJson(m_drawIconTypes));
        }
    }
}
