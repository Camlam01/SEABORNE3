using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Systems;

public static class TranceSystem
{
    public static int GetDecayAmount(int tranceResistanceStacks)
    {
        return Math.Max(1, 1 + tranceResistanceStacks);
    }

    public static async Task TickEnemyAtPlayerTurnStart(object enemy)
    {
        int trance = SeaborneReflectionTools.GetPowerStacks(enemy, TrancePower.Id);
        if (trance <= 0)
        {
            return;
        }

        int resistance = SeaborneReflectionTools.GetPowerStacks(enemy, TranceResistancePower.Id);
        int loss = Math.Min(trance, GetDecayAmount(resistance));

        await SeabornePowerTools.ApplyPowerToTarget(enemy, TrancePower.Id, -loss);
        await SeabornePowerTools.ApplyPowerToTarget(enemy, TranceResistancePower.Id, 1);
    }

    public static bool ShouldFreezeEnemyTurn(object enemy)
    {
        return SeaborneReflectionTools.GetPowerStacks(enemy, TrancePower.Id) > 0;
    }
}
