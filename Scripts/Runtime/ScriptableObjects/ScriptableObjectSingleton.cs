using UnityEngine;

namespace UrbanFox
{
    public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
    {
        private static T m_instance;

        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = Resources.Load<T>(string.Empty);
                    if (m_instance == null)
                    {
                        m_instance = CreateInstance<T>();
                        FoxyLogger.Log($"A new instance of {typeof(T).Name} has been created in runtime. A runtime-generated instance will only be saved as an asset in Editor.");
#if UNITY_EDITOR
                        var newAssetPath = UnityEditor.AssetDatabase.GenerateUniqueAssetPath($"Assets/Resources/{typeof(T).Name}.asset");
                        UnityEditor.AssetDatabase.CreateAsset(m_instance, newAssetPath);
                        FoxyLogger.Log($"A new instance of {typeof(T).Name} has been saved to the asset at {newAssetPath}.");
#endif
                    }
                }
                return m_instance;
            }
        }
    }
}
