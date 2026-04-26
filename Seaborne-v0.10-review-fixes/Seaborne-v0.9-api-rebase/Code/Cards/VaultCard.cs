using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class VaultCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;

    public VaultCard() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneCardTools.GainGemSlot(ModifyGemStacks(Upgraded ? 3 : 2));
        await ApplyGemWetIfAny();
    }
}
