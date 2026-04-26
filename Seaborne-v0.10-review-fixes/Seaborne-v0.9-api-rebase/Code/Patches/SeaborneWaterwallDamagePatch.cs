using System.Reflection;
using HarmonyLib;
using Seaborne.Powers;
using Seaborne.Systems;
using Seaborne.Utils;

namespace Seaborne.Patches;

[HarmonyPatch]
public static class SeaborneWaterwallDamagePatch
{
    public static IEnumerable<MethodBase> TargetMethods()
    {
        return AccessTools.AllTypes()
            .Where(type => type.FullName is not null
                           && type.FullName.Contains("MegaCrit.Sts2", StringComparison.Ordinal)
                           && !type.FullName.Contains("Seaborne", StringComparison.Ordinal))
            .SelectMany(type => type.GetMethods(AccessTools.all))
            .Where(method => method.Name.Contains("Damage", StringComparison.OrdinalIgnoreCase)
                             || method.Name.Contains("TakeDamage", StringComparison.OrdinalIgnoreCase)
                             || method.Name.Contains("ReceiveDamage", StringComparison.OrdinalIgnoreCase));
    }

    public static void Prefix(object __instance, object[] __args)
    {
        object? player = SeaborneReflectionTools.GetPlayer();
        if (player is null || !ReferenceEquals(__instance, player))
        {
            return;
        }

        int waterwall = SeaborneReflectionTools.GetPowerStacks(player, WaterwallPower.Id);
        if (waterwall <= 0)
        {
            return;
        }

        for (int index = 0; index < __args.Length; index++)
        {
            if (__args[index] is not int incomingDamage)
            {
                continue;
            }

            int resolvedDamage = WaterwallSystem.ResolveAttackDamage(incomingDamage, waterwall);
            if (resolvedDamage == incomingDamage)
            {
                continue;
            }

            __args[index] = resolvedDamage;
            return;
        }
    }
}
