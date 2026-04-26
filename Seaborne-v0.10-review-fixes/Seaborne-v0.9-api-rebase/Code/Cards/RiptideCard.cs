using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class RiptideCard : SeaborneCard
{
    public RiptideCard() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.None) { }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();
        object? player = SeaborneReflectionTools.GetPlayer();
        int handSize = (SeaborneReflectionTools.GetMemberValue(player!, "Hand") as System.Collections.ICollection)?.Count ?? 0;
        await BlockCmd.GainBlock(handSize * (Upgraded ? 3 : 2)).FromCard(this).Execute(choiceContext);
    }
}
