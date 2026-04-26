using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class BejeweledCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;

    public BejeweledCard() : base(1, CardType.Skill, CardRarity.Rare, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneCardTools.AcquireGem(RubyGemPower.Id);
        await SeaborneCardTools.AcquireGem(SapphireGemPower.Id);
        await SeaborneCardTools.AcquireGem(EmeraldGemPower.Id);
        await SeaborneCardTools.AcquireGem(AmberGemPower.Id);
        await SeaborneCardTools.AcquireGem(OpalGemPower.Id);
        await SeaborneCardTools.AcquireGem(DiamondGemPower.Id);
        await ApplyGemWetIfAny();
    }

    protected override void OnUpgrade()
    {
        Cost = 0;
    }
}
