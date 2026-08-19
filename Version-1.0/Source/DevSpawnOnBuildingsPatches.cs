using HarmonyLib;
using System.Linq;
using Timberborn.Beavers;
using Timberborn.BeaversUI;
using Timberborn.BlockSystem;
using Timberborn.Bots;
using Timberborn.BotsUI;
using Timberborn.CameraSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.CursorToolSystem;
using Timberborn.InputSystem;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Calloatti.DevNoCtrl
{
  [HarmonyPatch(typeof(BeaverGeneratorTool), "PlaceBeavers")]
  public static class DevSpawnOnBuildingsPatches_BeaverGeneratorTool
  {
    public static bool Prefix(
        bool isChild,
        int count,
        CursorCoordinatesPicker ____cursorCoordinatesPicker,
        BeaverFactory ____beaverFactory,
        IRandomNumberGenerator ____randomNumberGenerator,
        InputService ____inputService)
    {
      // Vanilla check
      CursorCoordinates? cursorCoordinates = ____cursorCoordinatesPicker.Pick();
      if (!cursorCoordinates.HasValue) return false;

      // Default to vanilla terrain position
      Vector3 position = CoordinateSystem.GridToWorldCentered(cursorCoordinates.GetValueOrDefault().TileCoordinates);

      // Access the game's injected services directly (Requires Publicized DLLs)
      Ray gridRay = ____cursorCoordinatesPicker._cameraService.ScreenPointToRayInGridSpace(____inputService.MousePosition);
      Ray worldRay = CoordinateSystem.GridToWorld(gridRay);

      if (____cursorCoordinatesPicker._selectableObjectRaycaster.TryHitSelectableObjectIncludeTerrainStump(worldRay, out var hitObject, out RaycastHit raycastHit))
      {
        var blockObject = hitObject.GetComponent<BlockObject>();

        // Check if we hit a building and if any block in it is stackable
        if (blockObject != null && blockObject.PositionedBlocks.GetAllBlocks().Any(b => b.Stackable.IsStackable()))
        {
          position = raycastHit.point; // Use the exact world coordinate on the roof
        }
      }

      // Execute standard spawn loops directly
      for (int i = 0; i < count; i++)
      {
        float num = ____randomNumberGenerator.Range(0f, 1f);
        if (isChild)
        {
          ____beaverFactory.CreateChild(position, num);
        }
        else
        {
          ____beaverFactory.CreateAdult(position, num);
        }
      }

      return false; // Skip the vanilla method
    }
  }

  [HarmonyPatch(typeof(BotGeneratorTool), "PlaceBots")]
  public static class DevSpawnOnBuildingsPatches_BotGeneratorTool
  {
    public static bool Prefix(
        int count,
        CursorCoordinatesPicker ____cursorCoordinatesPicker,
        BotFactory ____botFactory,
        InputService ____inputService)
    {
      // Vanilla check
      CursorCoordinates? cursorCoordinates = ____cursorCoordinatesPicker.Pick();
      if (!cursorCoordinates.HasValue) return false;

      // Default to vanilla terrain position
      Vector3 position = CoordinateSystem.GridToWorldCentered(cursorCoordinates.GetValueOrDefault().TileCoordinates);

      // Access the game's injected services directly (Requires Publicized DLLs)
      Ray gridRay = ____cursorCoordinatesPicker._cameraService.ScreenPointToRayInGridSpace(____inputService.MousePosition);
      Ray worldRay = CoordinateSystem.GridToWorld(gridRay);

      if (____cursorCoordinatesPicker._selectableObjectRaycaster.TryHitSelectableObjectIncludeTerrainStump(worldRay, out var hitObject, out RaycastHit raycastHit))
      {
        var blockObject = hitObject.GetComponent<BlockObject>();

        // Check if we hit a building and if any block in it is stackable
        if (blockObject != null && blockObject.PositionedBlocks.GetAllBlocks().Any(b => b.Stackable.IsStackable()))
        {
          position = raycastHit.point;
        }
      }

      // Execute standard spawn loops directly
      for (int i = 0; i < count; i++)
      {
        ____botFactory.Create(position);
      }

      return false; // Skip the vanilla method
    }
  }
}
