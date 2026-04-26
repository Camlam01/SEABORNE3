using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class HoarderCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    protected override IEnumerable<object> CanonicalVars => BlockVar(5m);

    public HoarderCard() : base(1, CardType.Skill, CardRarity.Common, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneCardTools.GainGemSlot();
        await BlockCmd.GainBlock(Upgraded ? 7m : DynamicVars.Block.BaseValue).FromCard(this).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(2m);
    }
}
