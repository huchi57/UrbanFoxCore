using System;
using UnityEngine;
using UnityEngine.UI;

namespace UrbanFox
{
    [RequireComponent(typeof(Text))]
    public class UILocalizedText : MonoBehaviour
    {
        [Serializable]
        public enum CapitalizationBehaviour
        {
            DontModify,
            AllUpperCase,
            AllLowerCase
        }

        [SerializeField, NonEditable]
        private Text m_text;

        [SerializeField, LocalizedString]
        private string m_key;

        [SerializeField]
        private CapitalizationBehaviour m_capitalization = CapitalizationBehaviour.DontModify;

        [Header("Editor Settings")]
        [SerializeField]
        private bool m_setGameObjectNameByKey = true;

        [Header("Key Missing Behaviour"), SerializeField]
        private bool m_highlightMissingKey = true;

        [SerializeField, ShowIf(nameof(m_highlightMissingKey), true)]
        private Color m_missingKeyColor = Color.red;

        public string Key
        {
            get => m_key;
            set
            {
                m_key = value;
                if (Application.isPlaying)
                {
                    LocalizeContent();
                }
            }
        }

        public CapitalizationBehaviour Captilization
        {
            get => m_capitalization;
            set
            {
                m_capitalization = value;
                if (Application.isPlaying)
                {
                    LocalizeContent();
                }
            }
        }

        private void OnValidate()
        {
            if (Application.isEditor && m_setGameObjectNameByKey)
            {
                gameObject.name = $"Text_{m_key}";
            }
            m_text = GetComponent<Text>();
        }

        private void OnEnable()
        {
            Localization.OnLanguageChanged += LocalizeText;
        }

        private void OnDisable()
        {
            Localization.OnLanguageChanged -= LocalizeText;
        }

        [ContextMenu(nameof(LocalizeText))]
        public void LocalizeText()
        {
            LocalizeContent();
        }

        [ContextMenu(nameof(LocalizeContent))]
        public void LocalizeContent()
        {
            if (m_key.TryGetLocalization(out var value))
            {
                switch (m_capitalization)
                {
                    case CapitalizationBehaviour.DontModify:
                        m_text.text = value;
                        return;
                    case CapitalizationBehaviour.AllUpperCase:
                        m_text.text = value.ToUpper();
                        return;
                    case CapitalizationBehaviour.AllLowerCase:
                        m_text.text = value.ToLower();
                        return;
                    default:
                        m_text.text = value;
                        return;
                }
            }
            var errorText = $"!!{value}";
            if (m_highlightMissingKey)
            {
                m_text.text = errorText.Color(m_missingKeyColor);
            }
            else
            {
                m_text.text = errorText;
            }
        }
    }
}
