using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class RodStrangleCard : SeaborneCard
{
    public override bool HasAttackDamage => true;
    public override bool HasCastOrReel => true;
    public RodStrangleCard() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        object? player = SeaborneReflectionTools.GetPlayer();
        decimal cast = player is null ? 0m : SeaborneReflectionTools.GetPowerStacks(player, CastPower.Id);
        await DamageCmd.Attack(ModifyGemDamage(cast)).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);
        await SeaborneDiscardTools.Reel(ModifyGemReel(Upgraded ? 2 : 1));
        await ApplyGemWetIfAny();
    }
}
