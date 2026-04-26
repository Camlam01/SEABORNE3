using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class PullThroughCard : SeaborneCard
{
    public override bool HasCastOrReel => true;
    protected override IEnumerable<object> CanonicalVars => BlockVar(10m);

    public PullThroughCard() : base(2, CardType.Skill, CardRarity.Common, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneDiscardTools.Reel(ModifyGemReel(Upgraded ? 4 : 3));
        await BlockCmd.GainBlock(DynamicVars.Block.BaseValue).FromCard(this).Execute(choiceContext);
    }
}
