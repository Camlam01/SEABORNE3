using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace Seaborne.Powers;

public sealed class ExplosiveGunpowderPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Intensity;
    public const string Id = "Seaborne:ExplosiveGunpowder";

    public string Name => "Explosive Gunpowder";
    public string Description => "Cannonballs deal double damage.";
    public override string? CustomPackedIconPath => "res://Assets/cannon_placeholder.png";
    public override string? CustomBigIconPath => CustomPackedIconPath;
    public override string? CustomBigBetaIconPath => CustomPackedIconPath;
}
