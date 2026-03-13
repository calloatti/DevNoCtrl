https://steamcommunity.com/sharedfiles/filedetails/?id=3682304469

DevNoCtrl (Developer Modifier Inverter)

Overview for Modders

DevNoCtrl is a workflow-enhancement mod for Timberborn modders and map developers. It globally 
inverts the logic of specific "developer modifier keys" (usually Ctrl, Alt, or Shift) used by the 
game's internal tools.

Instead of forcing the user to hold down modifier keys to execute instant/developer actions, this 
mod makes the "instant" actions the default behavior. Holding the modifier key will instead revert 
the tool to its standard, vanilla behavior.

How It Works Under the Hood

To maintain maximum compatibility and avoid the pitfalls of patching generic base classes (like 
BlockObjectDeletionTool<T>) or internal services, this mod intercepts the input layer directly.

It uses a single Harmony Postfix on Timberborn.InputSystem.InputService.IsKeyHeld(string keyId).

If Dev Mode is currently active (CrashSceneLoader.DevModeEnabled is true), the patch intercepts 
queries for specific string IDs and applies a logical NOT (!) to the result.

Intercepted Input IDs

The mod specifically targets and inverts the following internal keybind IDs:

Building & Tech Tree:

PlaceFinished: Makes the building placement tool spawn finished buildings instantly by default 
instead of construction sites.

InstantUnlock: Makes clicking locked items in the tech tree unlock them instantly for free, 
bypassing the science cost and confirmation dialog.

Deletion & Demolition:

SkipDeleteConfirmation: Bypasses the "Are you sure?" confirmation dialog when deleting objects by 
default.

DontRecoverGoods: Causes deleted buildings to instantly vanish without spawning leftover 
recoverable material boxes on the ground.

Agriculture & Planting:

PlantGrown: Forces the planting tool (or brush) to place fully mature plants by default.

PlantSpawned: Forces the planting tool to place the default un-grown prefab.

PlantWithYield: Forces the planting tool to place plants that are immediately at maximum yield and 
ready for harvest.

Safety & Compatibility

Vanilla Safe: The patch explicitly checks CrashSceneLoader.DevModeEnabled. If Dev Mode is toggled 
off, the patch instantly returns the unmodified boolean. It is 100% safe to leave enabled during 
standard survival gameplay.

Mod Compatible: Because it intercepts InputService.IsKeyHeld, it automatically applies to any 
custom tools or mods that rely on these standard Timberborn Dev Mode key strings.

Highly Performant: The patch does not use Reflection (AccessTools) at runtime to fetch private 
fields or internal methods, resulting in zero overhead during block placement or deletion loops.