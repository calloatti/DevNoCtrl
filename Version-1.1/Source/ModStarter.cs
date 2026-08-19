using HarmonyLib;
using Timberborn.ModManagerScene;

namespace Calloatti.DevNoCtrl
{
  public class ModStarter : IModStarter
  {
    public void StartMod(IModEnvironment modEnvironment)
    {
      new Harmony("Calloatti.DevNoCtrl").PatchAll();
    }
  }
}
