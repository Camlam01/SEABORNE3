using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class PowerOfTheSeaCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public PowerOfTheSeaCard() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeabornePowerTools.ApplyPowerToPlayer("Energy", ModifyGemStacks(2));
    }

    protected override void OnUpgrade()
    {
        Cost = 1;
    }

    private async Task OnReeled()
    {
        await SeabornePowerTools.ApplyPowerToPlayer("Energy", ModifyGemStacks(1));
    }
}
