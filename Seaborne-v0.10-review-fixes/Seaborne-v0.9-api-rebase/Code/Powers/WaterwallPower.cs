using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace Seaborne.Powers;

public sealed class WaterwallPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Intensity;
    public const string Id = "Seaborne:Waterwall";

    public string Name => "Waterwall";
    public string Description => "Blocks attack damage only if the damage is less than or equal to Waterwall. Blocks nothing against larger hits. Removed at the start of your turn.";
    public override string? CustomPackedIconPath => "res://Assets/waterwall_placeholder.png";
    public override string? CustomBigIconPath => CustomPackedIconPath;
    public override string? CustomBigBetaIconPath => CustomPackedIconPath;
}
