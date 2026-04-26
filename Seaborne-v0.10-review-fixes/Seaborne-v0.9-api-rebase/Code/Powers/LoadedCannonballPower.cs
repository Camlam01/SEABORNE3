using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace Seaborne.Powers;

public sealed class LoadedCannonballPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Intensity;
    public const string Id = "Seaborne:LoadedCannonball";

    public string Name => "Loaded Cannonball";
    public string Description => "Tracks cannonballs loaded into the cannon for Seaborne v0.1.";
    public override string? CustomPackedIconPath => "res://Assets/cannon_placeholder.png";
    public override string? CustomBigIconPath => CustomPackedIconPath;
    public override string? CustomBigBetaIconPath => CustomPackedIconPath;
}
