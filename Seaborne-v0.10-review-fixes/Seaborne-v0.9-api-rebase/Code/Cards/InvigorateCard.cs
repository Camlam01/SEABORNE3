using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;
using Seaborne.Systems;

namespace Seaborne.Cards;

public sealed class InvigorateCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public override bool HasCastOrReel => true;
    public InvigorateCard() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        SeaborneGemSystem.RechargeAll();
        await SeaborneDiscardTools.Reel(ModifyGemReel(1));
    }

    protected override void OnUpgrade()
    {
        Cost = 0;
    }
}
