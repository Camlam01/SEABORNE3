using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace Seaborne.Powers;

public sealed class AmberGemPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Intensity;
    public const string Id = "Seaborne:Amber";

    public string Name => "Amber";
    public string Description => "Placeholder gem. The first suitable card each turn costs less and exhausts.";
    public override string? CustomPackedIconPath => "res://Assets/gem_placeholder.png";
    public override string? CustomBigIconPath => CustomPackedIconPath;
    public override string? CustomBigBetaIconPath => CustomPackedIconPath;
}
