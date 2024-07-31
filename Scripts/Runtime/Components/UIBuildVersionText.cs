using UnityEngine;
using UnityEngine.UI;

namespace UrbanFox.Drifted
{
    [RequireComponent(typeof(Text))]
    public class UIBuildVersionText : MonoBehaviour
    {
        [SerializeField, NonEditable]
        private Text m_text;

        [Header("Information to Display")]

        [SerializeField]
        private bool m_getProductName = true;

        [SerializeField]
        private bool m_getBuildCodeName = true;

        [SerializeField]
        private bool m_getPlatformName = true;

        [SerializeField]
        private bool m_getTotalBuildNumber = true;

        private void OnValidate()
        {
            m_text = GetComponent<Text>();
        }

        private void Start()
        {
            m_text.text = BuildInformation.Instance.GetBuildVersionText(m_getProductName, m_getBuildCodeName, m_getPlatformName, m_getTotalBuildNumber);
        }
    }
}
