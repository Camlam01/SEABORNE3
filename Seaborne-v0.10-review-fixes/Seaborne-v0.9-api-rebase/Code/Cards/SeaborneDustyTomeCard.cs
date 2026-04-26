using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using Seaborne.Powers;
using Seaborne.Utils;

namespace Seaborne.Cards;

public sealed class SeaborneDustyTomeCard : SeaborneCard
{
    public override bool HasBuffOrDebuffStacks => true;
    public override bool HasCastOrReel => true;

    public SeaborneDustyTomeCard() : base(0, CardType.Skill, CardRarity.Rare, TargetType.None)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await ApplySeaborneGemModifiers();

        await SeaborneCardTools.GainGemSlot(1);
        await SeaborneCardTools.AcquireGem(RandomGemPowerId());
        await SeaborneDiscardTools.AddCast(ModifyGemCast(Upgraded ? 4 : 3));
        await SeaborneDiscardTools.Reel(ModifyGemReel(Upgraded ? 4 : 3));
        await ApplyGemWetIfAny();
    }

    private static string RandomGemPowerId()
    {
        string[] gems =
        [
            RubyGemPower.Id,
            SapphireGemPower.Id,
            EmeraldGemPower.Id,
            AmberGemPower.Id,
            OpalGemPower.Id,
            DiamondGemPower.Id
        ];

        return gems[Random.Shared.Next(gems.Length)];
    }
}
