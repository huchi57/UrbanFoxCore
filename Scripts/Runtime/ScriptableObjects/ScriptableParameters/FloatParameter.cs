using System;
using UnityEngine;

namespace UrbanFox
{
    [Serializable]
    public class FloatParameter
    {
        [SerializeField]
        private ValueSource m_valueSource;

        [SerializeField]
        private float m_value;

        [SerializeField]
        private ScriptableFloatParameter m_parameterAsset;

        public float Value => m_valueSource == ValueSource.Direct ? m_value : m_parameterAsset ? m_parameterAsset.Value : default;

        public FloatParameter(float defaultValue)
        {
            m_valueSource = ValueSource.Direct;
            m_value = defaultValue;
        }
    }
}
