using HarmonyLib;

namespace SimplyStamina.Patches; 

/**
 * <summary>Patches the player class to disable encumbered stamina drain.</summary>
 */
[HarmonyPatch(typeof(Player), nameof(Player.IsEncumbered))]
public static class SimplyStaminaEncumberedPatch {

  /**
   * <summary>Disables stamina usage for moving while encumbered.</summary>
   * <param name="__instance">The patched player object.</param>
   * <param name="__result">The result of <see cref="Player.IsEncumbered"/>: Whether the player is encumbered.</param>
   * <seealso cref="Player.IsEncumbered"/>
   */
  // ReSharper disable InconsistentNaming
  // ReSharper disable once UnusedMember.Local
  private static void Postfix(ref Player __instance, ref bool __result) {
    if (__result && SimplyStaminaSettings.DisableEncumberedStaminaDrain)
      __instance.m_encumberedStaminaDrain = 0.0f;
  }
    
}
