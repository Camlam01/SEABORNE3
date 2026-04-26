using System.Collections;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Cards;
using Seaborne.Powers;

namespace Seaborne.Utils;

public static class SeaborneCardTools
{
    public static async Task AcquireGem(string gemPowerId)
    {
        object? player = SeaborneReflectionTools.GetPlayer();

        if (player is not null)
        {
            int currentGemCount =
                HasGem(player, RubyGemPower.Id) +
                HasGem(player, SapphireGemPower.Id) +
                HasGem(player, EmeraldGemPower.Id) +
                HasGem(player, AmberGemPower.Id) +
                HasGem(player, OpalGemPower.Id) +
                HasGem(player, DiamondGemPower.Id);

            int slotCount = Math.Max(1, SeaborneReflectionTools.GetPowerStacks(player, GemSlotPower.Id));

            if (SeaborneReflectionTools.GetPowerStacks(player, gemPowerId) > 0 || currentGemCount >= slotCount)
            {
                return;
            }
        }

        await SeabornePowerTools.ApplyPowerToPlayer(gemPowerId, 1);
    }

    private static int HasGem(object player, string gemPowerId)
    {
        return SeaborneReflectionTools.GetPowerStacks(player, gemPowerId) > 0 ? 1 : 0;
    }

    public static async Task GainWaterwall(int stacks)
    {
        await SeabornePowerTools.ApplyPowerToPlayer(WaterwallPower.Id, stacks);
    }

    public static async Task GainGemSlot(int stacks = 1)
    {
        await SeabornePowerTools.ApplyPowerToPlayer(GemSlotPower.Id, stacks);
    }

    public static async Task GainSlippery(int stacks)
    {
        string[] candidateIds = ["Slippery", "sts2:Slippery", "MegaCrit:Slippery", SlipperyPower.Id];

        foreach (string powerId in candidateIds)
        {
            try
            {
                await SeabornePowerTools.ApplyPowerToPlayer(powerId, stacks);
                return;
            }
            catch
            {
                // STS2 builds may expose enemy powers under different ids; fall through to the local fallback.
            }
        }
    }

    public static async Task ApplyTrance(object target, int stacks)
    {
        await SeabornePowerTools.ApplyPowerToTarget(target, TrancePower.Id, stacks);
    }


    public static async Task ApplyPowerToAllEnemies(string powerId, int stacks)
    {
        foreach (object enemy in SeaborneReflectionTools.GetEnemies())
        {
            await SeabornePowerTools.ApplyPowerToTarget(enemy, powerId, stacks);
        }
    }

    public static async Task ApplyWeak(object target, int stacks)
    {
        await SeabornePowerTools.ApplyPowerToTarget(target, "Weak", stacks);
    }

    public static async Task ApplyVulnerable(object target, int stacks)
    {
        await SeabornePowerTools.ApplyPowerToTarget(target, "Vulnerable", stacks);
    }

    public static async Task LoadCannonballsFromHand()
    {
        object? player = SeaborneReflectionTools.GetPlayer();
        if (player is null)
        {
            return;
        }

        IList<CardModel>? hand = GetMutableCardList(player, "Hand", "HandCards");
        IList<CardModel>? discard = GetMutableCardList(player, "DiscardPile", "Discard", "DiscardedCards");
        if (hand is null || discard is null)
        {
            return;
        }
        List<CardModel> cannonballs = hand.Where(card => card.GetType().Name.Contains("Cannonball", StringComparison.OrdinalIgnoreCase)).ToList();

        foreach (CardModel cannonball in cannonballs)
        {
            hand.Remove(cannonball);
            discard.Add(cannonball);
            await SeabornePowerTools.ApplyPowerToPlayer(LoadedCannonballPower.Id, 1);
        }
    }

    public static async Task AddCannonballsToHand(int count)
    {
        object? player = SeaborneReflectionTools.GetPlayer();
        if (player is null || count <= 0)
        {
            return;
        }

        IList<CardModel>? hand = GetMutableCardList(player, "Hand", "HandCards");
        if (hand is null)
        {
            return;
        }
        Type? cannonballType = Type.GetType("Seaborne.Cards.CannonballCard, Seaborne");

        if (cannonballType is null)
        {
            return;
        }

        for (int index = 0; index < count; index++)
        {
            if (Activator.CreateInstance(cannonballType) is CardModel card)
            {
                hand.Add(card);
            }
        }

        await Task.CompletedTask;
    }


    public static async Task FireCannon(PlayerChoiceContext choiceContext, CardPlay cardPlay, Seaborne.Cards.SeaborneCard source, bool reloadFired = false, decimal consecutiveBonus = 0m)
    {
        object? player = SeaborneReflectionTools.GetPlayer();
        int loaded = player is null ? 0 : SeaborneReflectionTools.GetPowerStacks(player, LoadedCannonballPower.Id);
        if (loaded <= 0)
        {
            await DamageCmd.Attack(source.ModifyExternalGemDamage(8m)).FromCard(source).Targeting(cardPlay.Target).Execute(choiceContext);
            return;
        }

        decimal multiplier = player is not null && SeaborneReflectionTools.GetPowerStacks(player, ExplosiveGunpowderPower.Id) > 0 ? 2m : 1m;

        for (int index = 0; index < loaded; index++)
        {
            decimal scaling = 1m + (consecutiveBonus * index);
            await DamageCmd.Attack(source.ModifyExternalGemDamage(20m * multiplier * scaling)).FromCard(source).Targeting(cardPlay.Target).Execute(choiceContext);
        }

        await DamageCmd.Attack(source.ModifyExternalGemDamage(8m)).FromCard(source).Targeting(cardPlay.Target).Execute(choiceContext);

        if (reloadFired)
        {
            await SeabornePowerTools.ApplyPowerToPlayer(LoadedCannonballPower.Id, loaded);
        }
        else if (player is not null)
        {
            SeaborneReflectionTools.SetPowerStacks(player, LoadedCannonballPower.Id, 0);
        }
    }

    public static async Task LoadCannonball(int stacks = 1)
    {
        await SeabornePowerTools.ApplyPowerToPlayer(LoadedCannonballPower.Id, stacks);
    }

    public static async Task ApplyImbued(int stacks)
    {
        await SeabornePowerTools.ApplyPowerToPlayer(ImbuedPower.Id, stacks);
    }

    private static IList<CardModel>? GetMutableCardList(object player, params string[] names)
    {
        foreach (string name in names)
        {
            object? value = SeaborneReflectionTools.GetMemberValue(player, name);
            if (value is IList<CardModel> typed)
            {
                return typed;
            }
        }

        return null;
    }
}
