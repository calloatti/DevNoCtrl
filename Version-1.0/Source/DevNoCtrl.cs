using HarmonyLib;
using System;
using System.Reflection;
using Timberborn.ErrorReporting;
using Timberborn.InputSystem;
using Timberborn.ModManagerScene;

namespace Calloatti.DevNoCtrl
{
  public class DevNoCtrl : IModStarter
  {
    private const string HarmonyId = "com.calloatti.devnoctrl";

    public void StartMod(IModEnvironment modEnvironment)
    {
      var harmony = new Harmony(HarmonyId);
      harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
  }

  // ==============================================================================
  // ULTIMATE PATCH: Inverts Dev Mode modifier keys universally
  // ==============================================================================
  // This intercepts the game's InputService whenever it checks a keybind by its ID string.
  [HarmonyPatch(typeof(InputService), "IsKeyHeld", new Type[] { typeof(string) })]
  static class Patch_InputService_IsKeyHeld
  {
    // __0 represents the first argument passed to the method (the string keyId)
    static void Postfix(string __0, ref bool __result)
    {
      // Only alter behavior if Dev Mode is currently enabled
      if (CrashSceneLoader.DevModeEnabled)
      {
        // Check if the game is asking about one of our target Dev actions
        if (__0 == "PlaceFinished" ||
            __0 == "InstantUnlock" ||
            __0 == "DontRecoverGoods" ||
            __0 == "PlantGrown" ||
            __0 == "PlantSpawned" ||
            __0 == "PlantWithYield" ||
            __0 == "SkipDeleteConfirmation")
        {
          // INVERT THE LOGIC: 
          // If key is NOT pressed, tell the game it IS pressed (Instant action).
          // If key IS pressed, tell the game it is NOT pressed (Standard action).
          __result = !__result;
        }
      }
    }
  }
}