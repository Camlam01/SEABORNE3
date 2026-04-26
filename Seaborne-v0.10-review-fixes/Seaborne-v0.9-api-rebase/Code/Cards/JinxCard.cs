using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class JinxCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public JinxCard() : base(2, CardType.Power, CardRarity.Rare, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeabornePowerTools.ApplyPowerToPlayer(JinxPower.Id, ModifyGemStacks(Upgraded ? 75 : 50));
        await ApplyGemWetIfAny();
    }

    protected override void OnUpgrade()
    {
        
    }
}
