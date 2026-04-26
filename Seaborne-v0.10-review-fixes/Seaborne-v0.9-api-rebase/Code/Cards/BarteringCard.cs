using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Systems;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class BarteringCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public override bool CanReceiveWet => false;

    public BarteringCard() : base(1, CardType.Power, CardRarity.Rare, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();

        object? player = SeaborneReflectionTools.GetPlayer();
        int removed = 0;

        if (player is not null)
        {
            foreach (SeaborneGemType gem in Enum.GetValues<SeaborneGemType>())
            {
                string powerId = SeaborneGemSystem.PowerIdFor(gem);
                int stacks = SeaborneReflectionTools.GetPowerStacks(player, powerId);

                if (stacks > 0)
                {
                    removed++;
                    await SeabornePowerTools.ApplyPowerToPlayer(powerId, -stacks);
                }
            }
        }

        await SeabornePowerTools.ApplyPowerToPlayer("Gold", removed * 15);
    }
}
