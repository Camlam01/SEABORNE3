using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class MasterbaitCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public MasterbaitCard() : base(2, CardType.Power, CardRarity.Uncommon, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeabornePowerTools.ApplyPowerToPlayer(MasterbaitPower.Id, ModifyGemStacks(Upgraded ? 1 : 1));
        await ApplyGemWetIfAny();
    }

    protected override void OnUpgrade()
    {
        Cost = 1;
    }
}
