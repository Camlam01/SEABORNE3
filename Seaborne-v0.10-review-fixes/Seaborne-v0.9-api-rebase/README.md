# The Seaborne

A swashbuckling siren character mod for Slay the Spire 2.

## v0.10 contents

- Full designed card list implemented as card classes.
- Cast / Reel / Wet helpers.
- Gem system wiring.
- Cannon loading and firing wiring.
- Waterwall and Trance systems.
- Slippery adapter that attempts to use the game's existing Slippery power ids before falling back to the local placeholder.

## Build notes

Set the MSBuild property `Sts2Dir` to your local Slay the Spire 2 Godot data directory before building. The default is `C:\Program Files (x86)\Steam\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64`.

```bash
dotnet build
```

Send the first build errors back for API alignment against your local STS2/BaseLib version.


## v0.7 notes

Added placeholder Seaborne relics and a Dusty Tome integration seam.

- `SeaborneDustyTomeCard` is the Seaborne unique Ancient-card reward.
- `SeaborneDustyTomeIntegration` uses Harmony/reflection to attach the card to likely Dusty Tome / Ancient Card reward paths.
- Placeholder relics are intentionally named `Placeholder Relic 01` etc. so they can be renamed/rebalanced later without changing mechanic wiring.


## v0.8 audit notes

Added localisation files under `Localization/en/`:

- `cards.json`
- `powers.json`
- `relics.json`
- `character.json`
- `keywords.json`

The project now has localisation coverage for every current card, power, relic, keyword, and character entry. Some source files still expose inline `Name`/`Description` overrides because the public STS2/BaseLib examples currently demonstrate direct model strings; wire these JSON keys into the official loader once the exact localisation API is confirmed in your local build.

Known areas that still need local build validation:

1. Exact STS2/BaseLib API names for zone movement, card creation, and power application.
2. Dusty Tome internal hook target; current integration uses a conservative Harmony/reflection seam.
3. Slippery uses an adapter that attempts existing game IDs before using the local fallback.
4. Wet reeled-card triggers need final binding to the engine event that moves cards from discard to hand.
5. Final balance, upgrade text, relic names, relic effects, and art.


## v0.10 fix notes

Applied the API/runtime-risk fixes from the review pass:

- Wet gem application now uses `WetCardPower.Id` instead of the stale `Seaborne:WetCard` string.
- Amber now performs runtime play effects: it refunds one energy when consumed and marks the played card as exhaust via reflected exhaust flags.
- RunState lookup now checks `MegaCrit.Sts2.Core.Runs.RunState` first, with older namespace fallbacks.
- Power application now prefers BaseLib `CommonActions.Apply*Power*` overloads by signature and falls back to local stack adjustment only when no compatible overload works.
- Seaborne power lookups now accept aliases for canonical ids, power class names, and full power class names.
- Hand/discard helpers no longer mutate copied `IEnumerable` lists. If a mutable zone list cannot be found, the operation safely no-ops instead of pretending to move cards.
- Added an explicit Waterwall damage interception patch seam.
- Turn-start hook now wraps async work in a safe exception boundary.
- README now uses the same `Sts2Dir` property as the project file.

Still needs local validation against your installed `sts2.dll`:

1. Exact BaseLib power application overload selected at runtime.
2. Exact hand/discard zone member names.
3. Exact player turn-start and damage hook methods.
4. Exact Dusty Tome reward hook.
5. Whether Amber should be represented as a true pre-play cost modifier instead of an immediate energy refund.
