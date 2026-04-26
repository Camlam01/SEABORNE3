using System.Collections;
using System.Reflection;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using Seaborne.Cards;
using Seaborne.Characters;

namespace Seaborne.Patches;

public static class SeaborneDustyTomeIntegration
{
    public static CardModel CreateCard()
    {
        return new SeaborneDustyTomeCard();
    }

    public static bool IsSeaborneRun(object? value)
    {
        if (value is null)
        {
            return false;
        }

        string typeName = value.GetType().FullName ?? value.GetType().Name;
        return typeName.Contains(nameof(TheSeaborneCharacter), StringComparison.OrdinalIgnoreCase)
               || typeName.Contains("Seaborne", StringComparison.OrdinalIgnoreCase);
    }

    public static void TryAddToRewardCollection(object? rewardCollection)
    {
        if (rewardCollection is null)
        {
            return;
        }

        CardModel card = CreateCard();

        switch (rewardCollection)
        {
            case IList<CardModel> typedCards:
                typedCards.Add(card);
                return;
            case IList list:
                list.Add(card);
                return;
        }

        MethodInfo? addMethod = rewardCollection.GetType()
            .GetMethods(BindingFlags.Instance | BindingFlags.Public)
            .FirstOrDefault(method => method.Name is "Add" or "Append"
                                      && method.GetParameters().Length == 1
                                      && method.GetParameters()[0].ParameterType.IsAssignableFrom(card.GetType()));

        addMethod?.Invoke(rewardCollection, [card]);
    }
}

[HarmonyPatch]
public static class SeaborneDustyTomeRewardPatch
{
    private static readonly string[] CandidateTypeNames =
    [
        "MegaCrit.Sts2.Core.Rewards.AncientCardReward",
        "MegaCrit.Sts2.Core.Rewards.CardReward",
        "MegaCrit.Sts2.Core.Relics.DustyTomeRelic",
        "MegaCrit.Sts2.Gameplay.Relics.DustyTomeRelic",
        "DustyTomeRelic"
    ];

    public static IEnumerable<MethodBase> TargetMethods()
    {
        foreach (string typeName in CandidateTypeNames)
        {
            Type? type = AccessTools.TypeByName(typeName);
            if (type is null)
            {
                continue;
            }

            foreach (MethodInfo method in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                bool nameLooksRelevant =
                    method.Name.Contains("Ancient", StringComparison.OrdinalIgnoreCase)
                    || method.Name.Contains("Tome", StringComparison.OrdinalIgnoreCase)
                    || method.Name.Contains("Reward", StringComparison.OrdinalIgnoreCase)
                    || method.Name.Contains("Card", StringComparison.OrdinalIgnoreCase);

                if (nameLooksRelevant)
                {
                    yield return method;
                }
            }
        }
    }

    public static void Postfix(object __result)
    {
        SeaborneDustyTomeIntegration.TryAddToRewardCollection(__result);
    }
}
