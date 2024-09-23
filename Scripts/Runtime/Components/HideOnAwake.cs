using System;
using UnityEngine;
using UnityEngine.UI;

namespace UrbanFox
{
    public class HideOnAwake : MonoBehaviour
    {
        [Serializable]
        public enum OnAwakeAction
        {
            DoNothing,
            SetGameObjectInactive,
            HideGraphic,
            HideMeshRenderer,
            DestroyGameObject,
            MakeMeshToCastShadowOnly
        }

        [SerializeField]
        private OnAwakeAction m_onAwake = OnAwakeAction.SetGameObjectInactive;

        [SerializeField, ShowIf(nameof(m_onAwake), OnAwakeAction.HideGraphic), NonEditable]
        private Graphic m_graphic;

        [SerializeField, ShowIf(nameof(m_onAwake), OnAwakeAction.HideMeshRenderer), NonEditable]
        private MeshRenderer m_meshRenderer;

        private void OnValidate()
        {
            m_graphic = GetComponent<Graphic>();
            m_meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Awake()
        {
            switch (m_onAwake)
            {
                case OnAwakeAction.DoNothing:
                    break;
                case OnAwakeAction.SetGameObjectInactive:
                    gameObject.SetActive(false);
                    break;
                case OnAwakeAction.HideGraphic:
                    if (m_graphic || TryGetComponent(out m_graphic))
                    {
                        m_graphic.enabled = false;
                    }
                    break;
                case OnAwakeAction.HideMeshRenderer:
                    if (m_meshRenderer || TryGetComponent(out m_meshRenderer))
                    {
                        m_meshRenderer.enabled = false;
                    }
                    break;
                case OnAwakeAction.DestroyGameObject:
                    Destroy(gameObject);
                    break;
                case OnAwakeAction.MakeMeshToCastShadowOnly:
                    if (m_meshRenderer || TryGetComponent(out m_meshRenderer))
                    {
                        m_meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
