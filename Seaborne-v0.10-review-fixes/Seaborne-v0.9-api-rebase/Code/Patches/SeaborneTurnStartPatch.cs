using System.Threading.Tasks;
using System.Reflection;
using HarmonyLib;
using Seaborne.Systems;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Patches;

[HarmonyPatch]
public static class SeaborneTurnStartPatch
{
    public static IEnumerable<MethodBase> TargetMethods()
    {
        return AccessTools.AllTypes()
            .SelectMany(type => type.GetMethods(AccessTools.all))
            .Where(method => method.Name.Contains("StartPlayerTurn", StringComparison.OrdinalIgnoreCase)
                             || method.Name.Contains("OnPlayerTurnStart", StringComparison.OrdinalIgnoreCase)
                             || method.Name.Contains("BeginPlayerTurn", StringComparison.OrdinalIgnoreCase));
    }

    public static void Postfix()
    {
        _ = RunPostfixSafely();
    }

    private static async Task RunPostfixSafely()
    {
        try
        {
        SeaborneGemSystem.RechargeAll();
        foreach (object enemy in SeaborneReflectionTools.GetEnemies())
        {
            await TranceSystem.TickEnemyAtPlayerTurnStart(enemy);
        }

        object? player = SeaborneReflectionTools.GetPlayer();
        if (player is not null)
        {
            await SeabornePowerTools.ApplyPowerToPlayer(WaterwallPower.Id, -SeaborneReflectionTools.GetPowerStacks(player, WaterwallPower.Id));
        }
        }
        catch
        {
            // Turn hooks are best-effort until exact STS2 hook names are validated locally.
        }
    }
}
