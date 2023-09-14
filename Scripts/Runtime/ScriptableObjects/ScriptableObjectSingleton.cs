using UnityEngine;

namespace UrbanFox
{
    public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
    {
        private static ScriptableObjectSingleton<T> m_instance;

        public static ScriptableObjectSingleton<T> Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = Resources.Load<ScriptableObjectSingleton<T>>(string.Empty);
                    if (m_instance == null)
                    {
                        m_instance = CreateInstance<ScriptableObjectSingleton<T>>();
                        FoxyLogger.Log($"A new instance of {typeof(ScriptableObjectSingleton<T>).Name} has been created in runtime. A runtime-generated instance will only be saved as an asset in Editor.");
#if UNITY_EDITOR
                        var newAssetPath = UnityEditor.AssetDatabase.GenerateUniqueAssetPath($"Assets/Resources/{typeof(ScriptableObjectSingleton<T>).Name}.asset");
                        UnityEditor.AssetDatabase.CreateAsset(m_instance, newAssetPath);
                        FoxyLogger.Log($"A new instance of {typeof(ScriptableObjectSingleton<T>).Name} has been saved to the asset at {newAssetPath}.");
#endif
                    }
                }
                return m_instance;
            }
        }
    }
}
