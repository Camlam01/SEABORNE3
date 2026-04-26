using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace Seaborne.Powers;

public sealed class TorrentsPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Intensity;
    public const string Id = "Seaborne:Torrents";

    public string Name => "Torrents";
    public string Description => "When Waterwall deflects an attack, returns that damage to the attacker.";
    public override string? CustomPackedIconPath => "res://Assets/waterwall_placeholder.png";
    public override string? CustomBigIconPath => CustomPackedIconPath;
    public override string? CustomBigBetaIconPath => CustomPackedIconPath;
}
