using Seaborne.Powers;

namespace Seaborne.Systems;

public static class WaterwallSystem
{
    public static bool TryAbsorbAttack(int incomingAttackDamage, int waterwallStacks)
    {
        return incomingAttackDamage > 0 && incomingAttackDamage <= waterwallStacks;
    }

    public static int ResolveAttackDamage(int incomingAttackDamage, int waterwallStacks)
    {
        return TryAbsorbAttack(incomingAttackDamage, waterwallStacks) ? 0 : incomingAttackDamage;
    }

    public static string GetPowerId()
    {
        return WaterwallPower.Id;
    }
}
