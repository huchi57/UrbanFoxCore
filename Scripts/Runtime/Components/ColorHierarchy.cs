using UnityEngine;

namespace UrbanFox
{
    public class ColorHierarchy : MonoBehaviour
    {
        public static readonly Color DefaultBackgroundColor = new Color(0.2196079f, 0.2196079f, 0.2196079f, 1f);
        public static readonly Color DefaultTextColor = new Color(0.8f, 0.8f, 0.8f, 1f);

        [SerializeField]
        private ColorParameter m_backgroundColor = new ColorParameter(DefaultBackgroundColor);

        [SerializeField]
        private ColorParameter m_textColor = new ColorParameter(DefaultTextColor);

        [SerializeField]
        private FontStyle m_fontStyle = FontStyle.Normal;

        public Color BackgroundColor => m_backgroundColor.Value;

        public Color TextColor => m_textColor.Value;

        public FontStyle FontStyle => m_fontStyle;
    }
}
