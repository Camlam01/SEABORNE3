using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class EntangleCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public override bool HasCastOrReel => true;
    public EntangleCard() : base(1, CardType.Skill, CardRarity.Common, TargetType.AllEnemies) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        await SeaborneDiscardTools.AddCast(ModifyGemCast(2));
        foreach (object enemy in cardPlay.Targets)
        {
            await SeabornePowerTools.ApplyPowerToTarget(enemy, "Weak", Upgraded ? 2 : 1);
        }
    }
}
