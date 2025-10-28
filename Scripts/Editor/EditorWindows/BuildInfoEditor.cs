using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace UrbanFox.Editor
{
    [CustomEditor(typeof(BuildInfo))]
    public class BuildInfoEditor : UnityEditor.Editor
    {
        private BuildInfo m_target;

        private void OnEnable()
        {
            m_target = (BuildInfo)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayoutExtensions.ColoredButton("Build Application", Color.green, GUILayout.Height(50)))
            {
                m_target.UpdateBuildInfo();
                BuildApplication(m_target);
            }

            EditorGUILayout.HelpBox($"Available keywords: {BuildInfo.k_ProductName}, {BuildInfo.k_CodeName}, {BuildInfo.k_BuidCount}, {BuildInfo.k_BuildDateTime}, {BuildInfo.k_Platform}, {BuildInfo.k_Backend}, {BuildInfo.k_Architecture}", MessageType.Info);
            EditorGUILayout.HelpBox($"Preview output name: {m_target.GenerateOutputPath()}", MessageType.None);
        }

        private void SetScriptingBackend(BuildInfo.ScriptingBackend backend)
        {
            PlayerSettings.SetScriptingBackend(NamedBuildTarget.Standalone, backend == BuildInfo.ScriptingBackend.Mono ? ScriptingImplementation.Mono2x : ScriptingImplementation.IL2CPP);
        }

        private BuildTarget SetPlatform(BuildInfo.BuildPlatform platform)
        {
            BuildTarget m_buildTarget = BuildTarget.StandaloneWindows;
            switch (platform)
            {
                case BuildInfo.BuildPlatform.Windows_x64:
                    m_buildTarget = BuildTarget.StandaloneWindows64;
                    break;
                case BuildInfo.BuildPlatform.Windows_x32:
                    m_buildTarget = BuildTarget.StandaloneWindows;
                    break;
                case BuildInfo.BuildPlatform.macOS_Intel_x64:
                    m_buildTarget = BuildTarget.StandaloneOSX;
                    break;
                case BuildInfo.BuildPlatform.macOS_AppleSilicon:
                    m_buildTarget = BuildTarget.StandaloneOSX;
                    break;
                case BuildInfo.BuildPlatform.macOS_Universal:
                    m_buildTarget = BuildTarget.StandaloneOSX;
                    break;
                default:
                    break;
            }
            return m_buildTarget;
        }

        public void BuildApplication(BuildInfo buildInfo)
        {
            SetScriptingBackend(buildInfo.PlatformToBuild.ScriptingBackend);
            var buildTarget = SetPlatform(buildInfo.PlatformToBuild.Platform);
            var outputPath = buildInfo.GenerateOutputPath();
            var buildReport = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, outputPath, buildTarget, buildInfo.BuildOptions);
            var summary = buildReport.summary;

            switch (summary.result)
            {
                case BuildResult.Unknown:
                    break;
                case BuildResult.Succeeded:
                    FoxyLogger.Log($"Build succeeded with {summary.totalSize} bytes, {summary.totalWarnings} warning(s), and {summary.totalErrors} error(s). Time span: {summary.totalTime.TotalSeconds} second(s).");
                    if (buildInfo.OpenFolderWhenBuildCompletes)
                    {
                        EditorUtility.RevealInFinder(outputPath);
                    }
                    break;
                case BuildResult.Failed:
                    FoxyLogger.LogError($"Build failed with {summary.totalWarnings} warning(s) and {summary.totalErrors} error(s). Time span: {summary.totalTime.TotalSeconds} second(s).");
                    break;
                case BuildResult.Cancelled:
                    FoxyLogger.Log($"Build cancelled.");
                    break;
                default:
                    break;
            }
        }
    }
}
