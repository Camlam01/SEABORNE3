using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class RubyRuseCard : SeaborneCard
{
    protected override IEnumerable<object> CanonicalVars => BlockVar(7m);

    public RubyRuseCard() : base(1, CardType.Skill, CardRarity.Common, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneCardTools.AcquireGem(RubyGemPower.Id);
        await BlockCmd.GainBlock(DynamicVars.Block.BaseValue).FromCard(this).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3m);
    }
}
