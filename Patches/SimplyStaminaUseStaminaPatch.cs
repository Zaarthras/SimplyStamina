using HarmonyLib;

namespace SimplyStamina.Patches; 

/**
 * <summary>Patches the player class to adjust stamina usage.</summary>
 * <seealso cref="Player.UseStamina"/>
 */
[HarmonyPatch(typeof(Player), nameof(Player.UseStamina))]
public static class SimplyStaminaUseStaminaPatch {

  #region Stamina Use Context Logic
  
  /**
   * <summary>Contexts in which stamina is used that are handled by this mod.</summary>
   */
  private enum StaminaUseContext {
    Unknown,
    Run,
    Jump,
    Sneak,
    UseTool,
  }
  
  /**
   * <summary>The current context in which stamina is used.</summary>
   */
  private static StaminaUseContext currentStaminaUseContext = StaminaUseContext.Unknown;

  [HarmonyPatch]
  private static class PlayerStaminaUseContextPatches {
    
    /**
     * <summary>
     * Switches the current context to <see cref="StaminaUseContext.Run"/> when <see cref="Player.CheckRun"/> is called.
     * </summary>
     */
    [HarmonyPatch(typeof(Player), nameof(Player.CheckRun))]
    [HarmonyPrefix]
    private static void RunPrefix() => currentStaminaUseContext = StaminaUseContext.Run;
    
    /**
     * <summary>
     * Switches the current context to <see cref="StaminaUseContext.Jump"/> when <see cref="Player.OnJump"/> is called.
     * </summary>
     */
    [HarmonyPatch(typeof(Player), nameof(Player.OnJump))]
    [HarmonyPrefix]
    public static void JumpPrefix() => currentStaminaUseContext = StaminaUseContext.Jump;
    
    /**
     * <summary>
     * Switches the current context to <see cref="StaminaUseContext.Sneak"/> when <see cref="Player.OnSneaking"/> is
     * called.
     * </summary>
     */
    [HarmonyPatch(typeof(Player), nameof(Player.OnSneaking))]
    [HarmonyPrefix]
    public static void SneakPrefix() => currentStaminaUseContext = StaminaUseContext.Sneak;
    
    /**
     * <summary>
     * Switches the current context to <see cref="StaminaUseContext.UseTool"/> when <see cref="Player.Repair"/> or
     * <see cref="Player.UpdatePlacement"/> is called.
     * </summary>
     */
    [HarmonyPatch(typeof(Player), nameof(Player.Repair))]
    [HarmonyPatch(typeof(Player), nameof(Player.UpdatePlacement))]
    [HarmonyPrefix]
    public static void UseToolPrefix() => currentStaminaUseContext = StaminaUseContext.UseTool;
    
    /**
     * <summary>
     * Switches the current context back to <see cref="StaminaUseContext.Unknown"/> after the stamina adjustment logic
     * has run.
     * </summary>
     */
    [HarmonyPatch(typeof(Player), nameof(Player.CheckRun))]
    [HarmonyPatch(typeof(Player), nameof(Player.OnJump))]
    [HarmonyPatch(typeof(Player), nameof(Player.OnSneaking))]
    [HarmonyPatch(typeof(Player), nameof(Player.Repair))]
    [HarmonyPatch(typeof(Player), nameof(Player.UpdatePlacement))]
    public static void Postfix() => currentStaminaUseContext = StaminaUseContext.Unknown;
    
  }
  
  #endregion Stamina Use Context Logic
  
  /**
   * <summary>Adjusts stamina usage for certain actions.</summary>
   * <param name="__instance">The patched player object.</param>
   * <param name="v">The amount of stamina used.</param>
   */
  // ReSharper disable once InconsistentNaming
  // ReSharper disable once UnusedMember.Local
  private static bool Prefix(ref Player __instance, ref float v) {

    var player = __instance;

    // Only handle player actions here:
    if (currentStaminaUseContext is StaminaUseContext.Unknown)
      return true;
    
    // Whether the player is noticed by any mob:
    var playerNoticed = player.IsSensed() || player.IsTargeted();

    // Check for all the options this mod alters:
    switch (currentStaminaUseContext, playerNoticed) {
      
      // Running while being noticed:
      case (StaminaUseContext.Run, true):
        v *= SimplyStaminaSettings.RunStaminaDrainModifier;
        break;

      // Running while being unnoticed:
      case (StaminaUseContext.Run, false):
        v *= SimplyStaminaSettings.RunUnnoticedStaminaDrainModifier;
        break;
      
      // Jumping while being noticed:
      case (StaminaUseContext.Jump, true):
        v *= SimplyStaminaSettings.JumpStaminaDrainModifier;
        break;
      
      // Jumping while being unnoticed:
      case (StaminaUseContext.Jump, false):
        v *= SimplyStaminaSettings.JumpUnnoticedStaminaDrainModifier;
        break;
      
      // Sneaking while being noticed:
      case (StaminaUseContext.Sneak, true):
        v *= SimplyStaminaSettings.SneakStaminaDrainModifier;
        break;
      
      // Sneaking while being unnoticed:
      case (StaminaUseContext.Sneak, false):
        v *= SimplyStaminaSettings.SneakUnnoticedStaminaDrainModifier;
        break;
      
      // Using a tool:
      case (StaminaUseContext.UseTool, _):
        
        // The name of the item the player used:
        var usedItemName = player.GetRightItem()?.m_shared?.m_name;

        if (
          (usedItemName is "$item_hammer" && SimplyStaminaSettings.DisableHammerStaminaUsage)
          || (usedItemName is "$item_hoe" && SimplyStaminaSettings.DisableHoeStaminaUsage)
          || (usedItemName is "$item_cultivator" && SimplyStaminaSettings.DisableCultivatorStaminaUsage)
        ) {
          // Do not even execute Player.UseStamina() here:
          return false;
        }
        break;
      
    }
    
    return true;
  }
    
}
