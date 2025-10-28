using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanFox
{
    [CreateAssetMenu(menuName = "OwO/Build Info")]
    public class BuildInfo : ScriptableObjectSingleton<BuildInfo>
    {
        public const string k_ProductName = "$PRODUCTNAME";
        public const string k_CodeName = "$CODENAME";
        public const string k_BuidCount = "$BUILDCOUNT";
        public const string k_BuildDateTime = "$BUILDDATETIME";
        public const string k_Platform = "$PLATFORM";
        public const string k_Architecture = "$ARCHITECTURE";
        public const string k_Backend = "$BACKEND";

        [Serializable]
        public enum BuildPlatform
        {
            [InspectorName("Windows x64")]
            Windows_x64,
            [InspectorName("Windows x32")]
            Windows_x32,
            [InspectorName("macOS Intel x64")]
            macOS_Intel_x64,
            [InspectorName("macOS Apple Silicon")]
            macOS_AppleSilicon,
            [InspectorName("macOS Universal")]
            macOS_Universal
        }

        [Serializable]
        public enum ScriptingBackend
        {
            Mono,
            IL2CPP
        }

        [Serializable]
        public struct BuildPlatformParameters
        {
            public BuildPlatform Platform;
            public ScriptingBackend ScriptingBackend;
        }

        [Serializable]
        public class PlatformBuildCounter
        {
            public BuildPlatform Platform;
            public uint BuildCount;
        }

        [SerializeField]
        private string m_codeName;

        [SerializeField]
        private bool m_autoIncrementBuildCount = true;

        [SerializeField, Indent, EnableIf(nameof(m_autoIncrementBuildCount), false)]
        private uint m_buildCount;

        [SerializeField, NonEditable]
        private string m_buildDateTime;

        [SerializeField, NonEditable]
        private List<PlatformBuildCounter> m_platformBuildCounter = new List<PlatformBuildCounter>();

        [Header("In-Game Settings")]
        [SerializeField]
        private bool m_enableDebugFunctions;

        [Header("Build Settings")]
        [SerializeField]
        private string m_baseOutputFolder = "Builds";

        [SerializeField]
        private string m_buildPath;

        [SerializeField]
        private bool m_openFolderWhenBuildCompletes = true;

        [SerializeField]
        private BuildPlatformParameters m_platformToBuild;

#if UNITY_EDITOR
        [SerializeField]
        private UnityEditor.BuildOptions m_buildOptions;
#endif

        public string CodeName => m_codeName;
        public uint BuildCount => m_buildCount;
        public string BuildDateTime => m_buildDateTime;
        public RuntimePlatform Platform => Application.platform;
        public bool EnableDebugFunctions => m_enableDebugFunctions;
        public string BaseOutputFolder => m_baseOutputFolder;
        public string BuildPath => m_buildPath;
        public bool OpenFolderWhenBuildCompletes => m_openFolderWhenBuildCompletes;
        public BuildPlatformParameters PlatformToBuild => m_platformToBuild;

#if UNITY_EDITOR
        public UnityEditor.BuildOptions BuildOptions => m_buildOptions;
#endif

        public void UpdateBuildInfo()
        {
            if (m_autoIncrementBuildCount)
            {
                m_buildCount++;
            }
            bool hasPlatformDataFound = false;
            foreach (var data in m_platformBuildCounter)
            {
                if (data.Platform == m_platformToBuild.Platform)
                {
                    data.BuildCount++;
                    hasPlatformDataFound = true;
                }
            }
            if (!hasPlatformDataFound)
            {
                m_platformBuildCounter.Add(new PlatformBuildCounter()
                {
                    Platform = m_platformToBuild.Platform,
                    BuildCount = 1
                });
            }
            m_buildDateTime = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            SetAssetDirty();
        }

        private void SetAssetDirty()
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        public string GenerateOutputPath()
        {
            var subPath = ParseContentWithTokens(m_buildPath);
            switch (m_platformToBuild.Platform)
            {
                case BuildPlatform.Windows_x64:
                case BuildPlatform.Windows_x32:
                    subPath = Path.Combine(subPath, $"{Application.productName}.exe");
                    break;
                case BuildPlatform.macOS_Intel_x64:
                case BuildPlatform.macOS_AppleSilicon:
                case BuildPlatform.macOS_Universal:
                    subPath = Path.Combine(subPath, $"{Application.productName}.app");
                    break;
                default:
                    break;
            }
            var path = Path.Combine(m_baseOutputFolder, subPath);
            return path;
        }

        private string ParseContentWithTokens(string content)
        {
            return content.Replace(k_ProductName, Application.productName)
                .Replace(k_CodeName, m_codeName)
                .Replace(k_BuidCount, m_buildCount.ToString())
                .Replace(k_BuildDateTime, m_buildDateTime)
                .Replace(k_Platform, PlatformToString(m_platformToBuild.Platform))
                .Replace(k_Architecture, PlatformToArchitectureString(m_platformToBuild.Platform))
                .Replace(k_Backend, m_platformToBuild.ScriptingBackend.ToString());
        }

        private string PlatformToString(BuildPlatform buildPlatform)
        {
            switch (buildPlatform)
            {
                case BuildPlatform.Windows_x64:
                    return "Windows";
                case BuildPlatform.Windows_x32:
                    return "Windows";
                case BuildPlatform.macOS_Intel_x64:
                    return "macOS";
                case BuildPlatform.macOS_AppleSilicon:
                    return "macOS";
                case BuildPlatform.macOS_Universal:
                    return "macOS";
                default:
                    return "None";
            }
        }

        private string PlatformToArchitectureString(BuildPlatform buildPlatform)
        {
            switch (buildPlatform)
            {
                case BuildPlatform.Windows_x64:
                    return "x64";
                case BuildPlatform.Windows_x32:
                    return "x32";
                case BuildPlatform.macOS_Intel_x64:
                    return "Intel_x64";
                case BuildPlatform.macOS_AppleSilicon:
                    return "AppleSilicon";
                case BuildPlatform.macOS_Universal:
                    return "Universal";
                default:
                    return "None";
            }
        }
    }
}
