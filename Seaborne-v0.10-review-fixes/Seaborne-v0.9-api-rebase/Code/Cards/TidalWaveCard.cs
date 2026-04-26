using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class TidalWaveCard : SeaborneCard
{
    public override bool HasAttackDamage => true;
    public TidalWaveCard() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        object? player = SeaborneReflectionTools.GetPlayer();
        int handSize = (SeaborneReflectionTools.GetMemberValue(player!, "Hand") as System.Collections.ICollection)?.Count ?? 0;
        int damage = handSize * (Upgraded ? 3 : 2);
        await DamageCmd.Attack(ModifyGemDamage(damage)).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);
    }

    private async Task OnReeled()
    {
        await Task.CompletedTask;
    }
}
