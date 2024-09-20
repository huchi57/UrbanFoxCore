using System;
using UnityEngine;

namespace UrbanFox
{
    [Serializable]
    public class Vector3Parameter
    {
        [SerializeField]
        private ValueSource m_valueSource;

        [SerializeField]
        private Vector3 m_value;

        [SerializeField]
        private ScriptableVector3Parameter m_parameterAsset;

        public Vector3 Value => m_valueSource == ValueSource.Direct ? m_value : m_parameterAsset ? m_parameterAsset.Value : default;

        public Vector3Parameter(Vector3 defaultValue)
        {
            m_valueSource = ValueSource.Direct;
            m_value = defaultValue;
        }
    }
}
