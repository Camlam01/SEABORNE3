using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace Seaborne.Powers;

public sealed class TrancePower : CustomPowerModel
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Intensity;
    public const string Id = "Seaborne:Trance";

    public string Name => "Trance";
    public string Description => "Enemy intent is interrupted while Trance remains. At the start of the player turn, loses 1 plus Trance Resistance, then gains 1 Trance Resistance.";
    public override string? CustomPackedIconPath => "res://Assets/trance_placeholder.png";
    public override string? CustomBigIconPath => CustomPackedIconPath;
    public override string? CustomBigBetaIconPath => CustomPackedIconPath;
}
