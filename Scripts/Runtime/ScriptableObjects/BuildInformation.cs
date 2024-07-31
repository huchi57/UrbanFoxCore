using System;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanFox
{
    public class BuildInformation : ScriptableObjectSingleton<BuildInformation>
    {
        [Serializable]
        public class PlatformBuildData
        {
            public RuntimePlatform Platform;
            public string LastBuildTime;
            public int PlatformBuildNumber;
        }

        [SerializeField]
        private string m_buildCodeName;

        [Header("Auto-Generated Data (Edit with caution)")]
        [SerializeField]
        private int m_totalBuildNumber = 0;

        [SerializeField]
        private List<PlatformBuildData> m_platformBuildData = new List<PlatformBuildData>();

        private string TimeNow => DateTime.Now.ToString("yyyyMMd-HHmmss");

        public string GetBuildVersionText(bool getProductName = true, bool getBuildCodeName = true, bool getPlatformName = true, bool getTotalBuildNumber = true)
        {
            var texts = new List<string>();
            if (Application.isEditor)
            {
                if (getProductName)
                {
                    texts.Add(Application.productName);
                }
                if (getBuildCodeName)
                {
                    texts.Add($"Build Code Name: {m_buildCodeName}");
                }
                if (getPlatformName)
                {
                    texts.Add($"Platform: {Application.platform}\nBuild On: {TimeNow}");
                }
                if (getTotalBuildNumber)
                {
                    texts.Add($"Total Build Number: {m_totalBuildNumber}");
                }
                return string.Join(" | ", texts);
            }
            foreach (var data in m_platformBuildData)
            {
                if (Application.platform == data.Platform)
                {
                    if (getProductName)
                    {
                        texts.Add(Application.productName);
                    }
                    if (getBuildCodeName)
                    {
                        texts.Add($"Build Code Name: {m_buildCodeName}");
                    }
                    if (getPlatformName)
                    {
                        texts.Add($"Platform: {data.Platform}\nBuild #{data.PlatformBuildNumber} On: {data.LastBuildTime}");
                    }
                    if (getTotalBuildNumber)
                    {
                        texts.Add($"Total Build Number: {m_totalBuildNumber}");
                    }
                    return string.Join(" | ", texts);
                }
            }
            return string.Empty;
        }

        public string GetBuildID()
        {
            foreach (var data in m_platformBuildData)
            {
                if (Application.platform == data.Platform)
                {
                    return $"{m_buildCodeName}_{data.Platform}_build{data.PlatformBuildNumber}_{data.LastBuildTime}";
                }
            }
            return string.Empty;
        }

        public void UpdateLastBuildTime(RuntimePlatform platform)
        {
            m_totalBuildNumber++;
            var lastBuildTimeAsString = TimeNow;
            foreach (var data in m_platformBuildData)
            {
                if (platform == data.Platform)
                {
                    data.LastBuildTime = lastBuildTimeAsString;
                    data.PlatformBuildNumber++;
                    return;
                }
            }
            m_platformBuildData.Add(new PlatformBuildData()
            {
                Platform = platform,
                LastBuildTime = lastBuildTimeAsString,
                PlatformBuildNumber = 1
            });
        }
    }
}
