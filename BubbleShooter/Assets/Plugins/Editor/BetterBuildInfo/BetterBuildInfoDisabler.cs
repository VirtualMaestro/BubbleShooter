using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

[InitializeOnLoad]
public static class BuildInfoDisabler
{
    static BuildInfoDisabler()
    {
        BetterBuildInfo.ForceEnabledFlag = null;

#if BETTERBUILDINFO_DISABLED
#warning BETTERBUILDINFO_DISABLED is deprecated. Use BETTERBUILDINFO_FORCE_DISABLED instead.
        BetterBuildInfo.ForceEnabledFlag = false;
#endif

#if BETTERBUILDINFO_FORCE_DISABLED
        BetterBuildInfo.ForceEnabledFlag = false;
#elif BETTERBUILDINFO_FORCE_ENABLED
        BetterBuildInfo.ForceEnabledFlag = true;
#endif

        BetterBuildInfo.IsCloudBuild = false;
#if UNITY_CLOUD_BUILD
        BetterBuildInfo.IsCloudBuild = true;
#endif
    }
}
