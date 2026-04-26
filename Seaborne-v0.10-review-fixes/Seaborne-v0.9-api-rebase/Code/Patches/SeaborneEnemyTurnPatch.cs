using System.Reflection;
using HarmonyLib;
using Seaborne.Powers;
using Seaborne.Systems;
using Seaborne.Utils;

namespace Seaborne.Patches;

[HarmonyPatch]
public static class SeaborneEnemyTurnPatch
{
    public static IEnumerable<MethodBase> TargetMethods()
    {
        return AccessTools.AllTypes()
            .SelectMany(type => type.GetMethods(AccessTools.all))
            .Where(method => method.Name.Contains("EnemyTurn", StringComparison.OrdinalIgnoreCase)
                             || method.Name.Contains("TakeTurn", StringComparison.OrdinalIgnoreCase));
    }

    public static bool Prefix(object __instance)
    {
        if (!__instance.GetType().Name.Contains("Enemy", StringComparison.OrdinalIgnoreCase)
            && !__instance.GetType().Name.Contains("Monster", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return !TranceSystem.ShouldFreezeEnemyTurn(__instance);
    }
}
