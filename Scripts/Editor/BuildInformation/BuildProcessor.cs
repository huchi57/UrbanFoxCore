using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace UrbanFox.Editor
{
    class BuildProcessor : IPreprocessBuildWithReport
    {
        public int callbackOrder
        {
            get
            {
                return 0;
            }
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            var platform = BuildTargetConverter.TryConvertToRuntimePlatform(report.summary.platform);
            if (platform != null)
            {
                BuildInformation.Instance.UpdateLastBuildTime((RuntimePlatform)platform);
                EditorUtility.SetDirty(BuildInformation.Instance);
                AssetDatabase.Refresh();
            }
        }
    }
}
