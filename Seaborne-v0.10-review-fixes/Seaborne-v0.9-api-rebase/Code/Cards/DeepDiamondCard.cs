using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;
using Seaborne.Powers;

namespace Seaborne.Cards;

public sealed class DeepDiamondCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public override bool HasCastOrReel => true;
    public DeepDiamondCard() : base(2, CardType.Skill, CardRarity.Common, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneCardTools.AcquireGem(DiamondGemPower.Id);
        await SeaborneDiscardTools.AddCast(ModifyGemCast(3));
    }

    protected override void OnUpgrade()
    {
        Cost = 1;
    }
}
