using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using Seaborne.Characters;
using Seaborne.Systems;
using Seaborne.Utils;
using Seaborne.Powers;

namespace Seaborne.Cards;

[Pool(typeof(SeaborneCardPool))]
public abstract class SeaborneCard : CustomCardModel
{

    public virtual bool HasAttackDamage => false;
    public virtual bool HasBuffOrDebuffStacks => false;
    public virtual bool HasCastOrReel => false;
    public virtual bool CanReceiveWet => true;

    public decimal GemDamageMultiplier { get; set; } = 1m;
    public int GemStackBonus { get; set; }
    public int GemCostReduction { get; set; }
    public int GemAddedCast { get; set; }
    public int GemAddedReel { get; set; }
    public int GemAddedWet { get; set; }
    public bool GemShouldExhaust { get; set; }

    protected async Task ApplySeaborneGemModifiers()
    {
        ResetGemRuntimeModifiers();
        await SeaborneGemSystem.ApplyGemModifiers(this);
        await SeaborneGemSystem.ApplyRuntimePlayEffects(this);
    }

    protected decimal ModifyGemDamage(decimal baseDamage)
    {
        return Math.Ceiling(baseDamage * GemDamageMultiplier);
    }

    public decimal ModifyExternalGemDamage(decimal baseDamage)
    {
        return ModifyGemDamage(baseDamage);
    }

    protected int ModifyGemStacks(int baseStacks)
    {
        return Math.Max(0, baseStacks + GemStackBonus);
    }

    protected int ModifyGemCast(int baseCast)
    {
        return Math.Max(0, baseCast + GemAddedCast);
    }

    protected int ModifyGemReel(int baseReel)
    {
        return Math.Max(0, baseReel + GemAddedReel);
    }

    protected async Task ApplyGemWetIfAny()
    {
        if (GemAddedWet > 0)
        {
            await SeabornePowerTools.ApplyPowerToPlayer(WetCardPower.Id, GemAddedWet);
        }
    }

    private void ResetGemRuntimeModifiers()
    {
        GemDamageMultiplier = 1m;
        GemStackBonus = 0;
        GemCostReduction = 0;
        GemAddedCast = 0;
        GemAddedReel = 0;
        GemAddedWet = 0;
        GemShouldExhaust = false;
    }

    public override string PortraitPath => "res://Assets/card_placeholder.png";
    public override string BetaPortraitPath => PortraitPath;

    protected SeaborneCard(int cost, CardType type, CardRarity rarity, TargetType targetType)
        : base(cost, type, rarity, targetType)
    {
    }

    protected static IEnumerable<object> DamageVar(decimal amount)
    {
        return [new DamageVar(amount, ValueProp.Move)];
    }

    protected static IEnumerable<object> BlockVar(decimal amount)
    {
        return [new BlockVar(amount, ValueProp.Move)];
    }
}
