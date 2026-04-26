using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Systems;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class GemCannonCard : SeaborneCard
{
    public override bool HasAttackDamage => true;

    public GemCannonCard() : base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();

        object? player = SeaborneReflectionTools.GetPlayer();
        int gems = player is null
            ? 0
            : Enum.GetValues<SeaborneGemType>().Count(gem => SeaborneReflectionTools.GetPowerStacks(player, SeaborneGemSystem.PowerIdFor(gem)) > 0);

        for (int index = 0; index < gems; index++)
        {
            await DamageCmd.Attack(ModifyGemDamage(Upgraded ? 30m : 20m)).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);
        }

        await ApplyGemWetIfAny();
    }
}
