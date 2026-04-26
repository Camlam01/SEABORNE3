using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;
using Seaborne.Powers;

namespace Seaborne.Cards;

public sealed class OceanicOpalJewelCard : SeaborneCard
{
    public override bool HasCastOrReel => true;
    public OceanicOpalJewelCard() : base(2, CardType.Skill, CardRarity.Common, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneCardTools.AcquireGem(OpalGemPower.Id);
        await SeaborneDiscardTools.Reel(ModifyGemReel(2));
    }

    protected override void OnUpgrade()
    {
        Cost = 1;
    }
}
