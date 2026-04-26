using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace Seaborne.Powers;

public sealed class ShardyShrapnelPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Intensity;
    public const string Id = "Seaborne:ShardyShrapnel";

    public string Name => "Shardy Shrapnel";
    public string Description => "Cannon shots deal additional shrapnel damage.";
    public override string? CustomPackedIconPath => "res://Assets/cannon_placeholder.png";
    public override string? CustomBigIconPath => CustomPackedIconPath;
    public override string? CustomBigBetaIconPath => CustomPackedIconPath;
}
