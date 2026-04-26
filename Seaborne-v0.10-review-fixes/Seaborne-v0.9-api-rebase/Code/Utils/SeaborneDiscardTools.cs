using System.Collections;
using MegaCrit.Sts2.Core.Entities.Cards;
using Seaborne.Powers;

namespace Seaborne.Utils;

public static class SeaborneDiscardTools
{
    public static async Task AddCast(int stacks)
    {
        await SeabornePowerTools.ApplyPowerToPlayer(CastPower.Id, stacks);
    }

    public static async Task Reel(int count)
    {
        object? player = SeaborneReflectionTools.GetPlayer();
        if (player is null || count <= 0)
        {
            return;
        }

        int cast = SeaborneReflectionTools.GetPowerStacks(player, CastPower.Id);
        if (cast <= 0)
        {
            return;
        }

        IList<CardModel>? discard = GetMutableCardList(player, "DiscardPile", "Discard", "DiscardedCards");
        IList<CardModel>? hand = GetMutableCardList(player, "Hand", "HandCards");

        if (discard is null || hand is null || discard.Count == 0)
        {
            SeaborneReflectionTools.SetPowerStacks(player, CastPower.Id, 0);
            return;
        }

        int hookedIndex = Math.Clamp(discard.Count - cast, 0, discard.Count - 1);
        int finalIndex = Math.Min(hookedIndex + count - 1, discard.Count - 1);

        List<CardModel> reeled = [];
        for (int index = hookedIndex; index <= finalIndex; index++)
        {
            reeled.Add(discard[index]);
        }

        foreach (CardModel card in reeled)
        {
            discard.Remove(card);
            hand.Add(card);
            await TriggerWet(card);
        }

        SeaborneReflectionTools.SetPowerStacks(player, CastPower.Id, 0);
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

    private static async Task TriggerWet(CardModel card)
    {
        Type cardType = card.GetType();
        object? result = cardType
            .GetMethod("OnReeled", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.Invoke(card, []);

        if (result is Task task)
        {
            await task;
        }
    }
}
