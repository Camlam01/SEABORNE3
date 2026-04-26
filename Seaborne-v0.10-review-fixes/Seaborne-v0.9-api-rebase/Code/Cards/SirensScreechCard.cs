using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class SirensScreechCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public SirensScreechCard() : base(3, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneCardTools.ApplyTrance(cardPlay.Target!, ModifyGemStacks(Upgraded ? 4 : 3));
    }

    private async Task OnReeled()
    {
        object? player = SeaborneReflectionTools.GetPlayer();
        if (player is not null)
        {
            await SeabornePowerTools.ApplyPowerToPlayer("Seaborne:SirenEcho", ModifyGemStacks(1));
        }
    }
}
