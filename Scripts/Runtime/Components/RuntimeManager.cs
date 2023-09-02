using UnityEngine;

namespace UrbanFox
{
    public class RuntimeManager<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T m_instance;
        private static bool m_isApplicationQuitting = false;

        public static bool IsInstanceExist => !m_isApplicationQuitting && m_instance != null;
        public static T Instance => GetInstance();

        public static T GetInstance()
        {
            if (m_isApplicationQuitting)
            {
                Debug.Log(2);
                FoxyLogger.LogWarning($"An instance of {typeof(T)} will not be returned because the application is quitting.");
                return null;
            }
            if (m_instance == null)
            {
                Debug.Log(3);
                m_instance = FindObjectOfType<T>();
                if (m_instance == null)
                {
                    Debug.Log(4);
                    m_instance = new GameObject($"[{typeof(T).Name}]").AddComponent<T>();
                    FoxyLogger.Log($"An instance of {typeof(T)} has been automatically created because an instance could not be found.");
                }
                Debug.Log(5);
                DontDestroyOnLoad(m_instance.transform.root);
            }
            Debug.Log(6);
            return m_instance;
        }

        public virtual void OnAwake()
        {
            Debug.Log(1);
            GetInstance();
        }

        public virtual void OnApplicationQuit()
        {
            m_isApplicationQuitting = false;
        }
    }
}
