# Seaborne v0.9 API Rebase Audit

Fixed in this package:

- `Seaborne.csproj` now targets the Godot STS2 data directory:
  `$(Sts2Dir)\sts2.dll` and `$(Sts2Dir)\0Harmony.dll`.
- `mod_manifest.json` now includes `id`, `has_pck`, `has_dll`, and `affects_gameplay`.
- `TheSeaborneCharacter` now follows the current `CustomCharacterModel` override pattern:
  `StartingHp`, `StartingDeck`, `StartingRelics`, `CardPool`, `RelicPool`, `PotionPool`, and visual path properties.
- Pool classes now inherit from `CustomCardPoolModel`, `CustomRelicPoolModel`, and `CustomPotionPoolModel`.
- Powers no longer call the old `CustomPowerModel` constructor; they override `Type`, `StackType`, and icon path properties.
- Relics no longer call the old `CustomRelicModel` constructor; they override `Rarity` and current icon path properties.
- Added minimal Godot project/PCK scaffolding:
  `project.godot`, `export_presets.cfg`, and placeholder `.tscn` scenes.
- `SeaborneTurnStartPatch` no longer uses `async void`.

Still needs local validation against your exact STS2/BaseLib build:

- `ModelDb.Card<T>()`, `ModelDb.Relic<T>()`, and pool helpers may need small generic/name adjustments if your BaseLib build differs from the Flandre reference.
- Reflection-based helper systems for zone movement, Dusty Tome, Slippery, and some power application paths are still provisional.
- Placeholder `.tscn` scenes are intentionally minimal; real character/select/rest/merchant scenes should replace them.
- First `dotnet build` errors should be used as the next source of truth.


## v0.10 review fixes

See README v0.10 fix notes. Remaining high-risk seams are exact STS2 hook names, Dusty Tome, and mutable zone members.
