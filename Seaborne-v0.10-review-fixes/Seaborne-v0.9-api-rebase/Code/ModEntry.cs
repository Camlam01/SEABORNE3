using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;

namespace Seaborne;

[ModInitializer("Init")]
public static class ModEntry
{
    private static Harmony? harmony;

    public static void Init()
    {
        Log.Warn("[Seaborne] Initializing...");
        harmony = new Harmony("com.camlam01.seaborne");
        harmony.PatchAll();
        Log.Warn("[Seaborne] Loaded.");
    }
}
