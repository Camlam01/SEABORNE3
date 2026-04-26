using Seaborne.Cards;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Systems;

public enum SeaborneGemType
{
    Ruby,
    Sapphire,
    Emerald,
    Amber,
    Opal,
    Diamond
}

public static class SeaborneGemSystem
{
    private static readonly HashSet<SeaborneGemType> ConsumedThisTurn = [];

    public static async Task ApplyGemModifiers(SeaborneCard card)
    {
        object? player = SeaborneReflectionTools.GetPlayer();
        if (player is null)
        {
            return;
        }

        foreach (SeaborneGemType gem in Enum.GetValues<SeaborneGemType>())
        {
            if (ConsumedThisTurn.Contains(gem) || !PlayerHasGem(player, gem) || !IsEligible(gem, card))
            {
                continue;
            }

            ApplyModifier(gem, card);
            ConsumedThisTurn.Add(gem);
        }

        if (card.GemAddedCast != 0)
        {
            await SeaborneDiscardTools.AddCast(card.GemAddedCast);
        }
    }


    public static async Task ApplyRuntimePlayEffects(SeaborneCard card)
    {
        if (card.GemCostReduction > 0)
        {
            await RefundAmberEnergy(card.GemCostReduction);
        }

        if (card.GemShouldExhaust)
        {
            MarkCardExhaustsAfterUse(card);
        }
    }

    private static async Task RefundAmberEnergy(int amount)
    {
        object? player = SeaborneReflectionTools.GetPlayer();
        if (player is null || amount <= 0)
        {
            return;
        }

        if (SeaborneReflectionTools.TryAddEnergy(player, amount))
        {
            return;
        }

        Type? energyCmdType = Type.GetType("MegaCrit.Sts2.Core.Commands.EnergyCmd, sts2");
        System.Reflection.MethodInfo? gainMethod = energyCmdType?
            .GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
            .FirstOrDefault(method => method.Name.Contains("Gain", StringComparison.OrdinalIgnoreCase)
                                      && method.GetParameters().Any(parameter => parameter.ParameterType == typeof(int)));

        object? result = gainMethod?.Invoke(null, [amount]);
        if (result is Task task)
        {
            await task;
        }
    }

    private static void MarkCardExhaustsAfterUse(SeaborneCard card)
    {
        foreach (string memberName in new[] { "Exhaust", "Exhausts", "ExhaustOnUse", "ExhaustAfterUse", "ShouldExhaust" })
        {
            if (SeaborneReflectionTools.SetMemberValue(card, memberName, true))
            {
                return;
            }
        }
    }

    public static void RechargeAll()
    {
        ConsumedThisTurn.Clear();
    }

    public static string PowerIdFor(SeaborneGemType gem)
    {
        return gem switch
        {
            SeaborneGemType.Ruby => RubyGemPower.Id,
            SeaborneGemType.Sapphire => SapphireGemPower.Id,
            SeaborneGemType.Emerald => EmeraldGemPower.Id,
            SeaborneGemType.Amber => AmberGemPower.Id,
            SeaborneGemType.Opal => OpalGemPower.Id,
            SeaborneGemType.Diamond => DiamondGemPower.Id,
            _ => throw new ArgumentOutOfRangeException(nameof(gem), gem, null)
        };
    }

    private static bool PlayerHasGem(object player, SeaborneGemType gem)
    {
        return SeaborneReflectionTools.GetPowerStacks(player, PowerIdFor(gem)) > 0;
    }

    private static bool IsEligible(SeaborneGemType gem, SeaborneCard card)
    {
        return gem switch
        {
            SeaborneGemType.Ruby => card.HasAttackDamage,
            SeaborneGemType.Sapphire => card.HasAttackDamage,
            SeaborneGemType.Emerald => card.HasBuffOrDebuffStacks,
            SeaborneGemType.Amber => true,
            SeaborneGemType.Opal => card.HasCastOrReel,
            SeaborneGemType.Diamond => card.CanReceiveWet,
            _ => false
        };
    }

    private static void ApplyModifier(SeaborneGemType gem, SeaborneCard card)
    {
        switch (gem)
        {
            case SeaborneGemType.Ruby:
            case SeaborneGemType.Sapphire:
                card.GemDamageMultiplier += 0.2m;
                break;
            case SeaborneGemType.Emerald:
                card.GemStackBonus += 1;
                break;
            case SeaborneGemType.Amber:
                card.GemCostReduction += 1;
                card.GemShouldExhaust = true;
                break;
            case SeaborneGemType.Opal:
                card.GemAddedCast += 1;
                card.GemAddedReel += 1;
                break;
            case SeaborneGemType.Diamond:
                card.GemAddedWet += 1;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gem), gem, null);
        }
    }
}
