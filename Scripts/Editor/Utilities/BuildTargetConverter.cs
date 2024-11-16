using UnityEditor;
using UnityEngine;

namespace UrbanFox.Editor
{
    // https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/BuildTargetConverter.cs
    public static class BuildTargetConverter
    {
        public static RuntimePlatform? TryConvertToRuntimePlatform(this BuildTarget buildTarget)
        {
            switch (buildTarget)
            {
                case BuildTarget.Android:
                    return RuntimePlatform.Android;
                case BuildTarget.PS4:
                    return RuntimePlatform.PS4;
                case BuildTarget.PS5:
                    return RuntimePlatform.PS5;
                case BuildTarget.StandaloneLinux64:
                    return RuntimePlatform.LinuxPlayer;
                case BuildTarget.LinuxHeadlessSimulation:
                    return RuntimePlatform.LinuxPlayer;
                case BuildTarget.StandaloneOSX:
                    return RuntimePlatform.OSXPlayer;
                case BuildTarget.StandaloneWindows:
                    return RuntimePlatform.WindowsPlayer;
                case BuildTarget.StandaloneWindows64:
                    return RuntimePlatform.WindowsPlayer;
                case BuildTarget.Switch:
                    return RuntimePlatform.Switch;
                case BuildTarget.WSAPlayer:
                    return RuntimePlatform.WSAPlayerARM;
                case BuildTarget.XboxOne:
                    return RuntimePlatform.XboxOne;
                case BuildTarget.iOS:
                    return RuntimePlatform.IPhonePlayer;
                case BuildTarget.tvOS:
                    return RuntimePlatform.tvOS;
                //case BuildTarget.VisionOS:
                //    return RuntimePlatform.VisionOS;
                case BuildTarget.WebGL:
                    return RuntimePlatform.WebGLPlayer;
                case BuildTarget.GameCoreXboxSeries:
                    return RuntimePlatform.GameCoreXboxSeries;
                case BuildTarget.GameCoreXboxOne:
                    return RuntimePlatform.GameCoreXboxOne;
                case BuildTarget.EmbeddedLinux:
                    return RuntimePlatform.EmbeddedLinuxArm64;
                case BuildTarget.QNX:
                    return RuntimePlatform.QNXArm64;
                default:
                    return null;
            }
        }
    }
}
