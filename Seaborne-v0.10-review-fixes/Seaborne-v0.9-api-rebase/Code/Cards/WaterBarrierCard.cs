using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class WaterBarrierCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public override bool HasCastOrReel => true;
    public WaterBarrierCard() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        object? player = SeaborneReflectionTools.GetPlayer();
        int cast = player is null ? 0 : SeaborneReflectionTools.GetPowerStacks(player, CastPower.Id);
        await SeabornePowerTools.ApplyPowerToPlayer("Seaborne:Waterwall", cast);
        await SeaborneDiscardTools.Reel(ModifyGemReel(Upgraded ? 2 : 1));
    }
}
