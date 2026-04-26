using Seaborne.Powers;

namespace Seaborne.Utils;

public static class SeabornePowerIdAliases
{
    private static readonly Dictionary<string, string[]> Aliases = new(StringComparer.OrdinalIgnoreCase)
    {
        [CastPower.Id] = BuildAliases<CastPower>(CastPower.Id),
        [WetCardPower.Id] = BuildAliases<WetCardPower>(WetCardPower.Id),
        [TrancePower.Id] = BuildAliases<TrancePower>(TrancePower.Id),
        [TranceResistancePower.Id] = BuildAliases<TranceResistancePower>(TranceResistancePower.Id),
        [WaterwallPower.Id] = BuildAliases<WaterwallPower>(WaterwallPower.Id),
        [GemSlotPower.Id] = BuildAliases<GemSlotPower>(GemSlotPower.Id),
        [RubyGemPower.Id] = BuildAliases<RubyGemPower>(RubyGemPower.Id),
        [SapphireGemPower.Id] = BuildAliases<SapphireGemPower>(SapphireGemPower.Id),
        [EmeraldGemPower.Id] = BuildAliases<EmeraldGemPower>(EmeraldGemPower.Id),
        [AmberGemPower.Id] = BuildAliases<AmberGemPower>(AmberGemPower.Id),
        [OpalGemPower.Id] = BuildAliases<OpalGemPower>(OpalGemPower.Id),
        [DiamondGemPower.Id] = BuildAliases<DiamondGemPower>(DiamondGemPower.Id),
        [LoadedCannonballPower.Id] = BuildAliases<LoadedCannonballPower>(LoadedCannonballPower.Id),
        [TorrentsPower.Id] = BuildAliases<TorrentsPower>(TorrentsPower.Id),
        [ExplosiveGunpowderPower.Id] = BuildAliases<ExplosiveGunpowderPower>(ExplosiveGunpowderPower.Id),
        [AutoCannonPower.Id] = BuildAliases<AutoCannonPower>(AutoCannonPower.Id)
    };

    public static IEnumerable<string> GetAliases(string canonicalId)
    {
        if (Aliases.TryGetValue(canonicalId, out string[] aliases))
        {
            return aliases;
        }

        string tail = canonicalId.Contains(':') ? canonicalId[(canonicalId.LastIndexOf(':') + 1)..] : canonicalId;
        return [canonicalId, tail];
    }

    private static string[] BuildAliases<TPower>(string canonicalId)
    {
        Type type = typeof(TPower);
        string shortName = type.Name;
        return
        [
            canonicalId,
            shortName,
            shortName.Replace("Power", "", StringComparison.OrdinalIgnoreCase),
            type.FullName ?? shortName
        ];
    }
}
