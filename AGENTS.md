Include ..\AGENTS.md

# DevNoCtrl — Mod-Specific Agent Instructions

## Identity
- **Assembly:** `devnoctrl`
- **Namespace:** `Calloatti.DevNoCtrl`
- **Framework:** Harmony (ID `"com.calloatti.devnoctrl"`)
- **Publicizer:** includes `Timberborn.BeaversUI`, `Timberborn.BotsUI`, `Timberborn.CursorToolSystem`
- **ModId:** `Calloatti.DevNoCtrl`
- **Min Game Version:** 1.0.12.5 — uses `timberborn-decompiled-1.0.*`

## What This Mod Does
Inverts Dev Mode key modifiers so that Instant actions (PlaceFinished, InstantUnlock, etc.) happen WITHOUT holding modifier keys, and holding the key enables standard behavior.

## Source Architecture (`Version-1.0/Source/`)

| File | Role |
|---|---|
| `DevNoCtrl.cs` | `IModStarter` entry point + `InputService.IsKeyHeld` inversion patch |
| `DevSpawnOnBuildings.cs` | Dev spawn-on-buildings helper |

## Hard Rule
DO NOT EVER TOUCH THE DEPLOY FOLDER.

BUILD DOES EVERYTHING, NEVER EVER MESS WITH THE DEPLOY PROCESS.
