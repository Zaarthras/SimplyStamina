using System.Diagnostics;
using HarmonyLib;

namespace SimplyStamina.Patches; 

/**
 * <summary>Patches the player class to adjust stamina usage.</summary>
 * <seealso cref="Player.UseStamina"/>
 */
[HarmonyPatch(typeof(Player), nameof(Player.UseStamina))]
public static class SimplyStaminaUseStaminaPatch {
  
  /**
   * <summary>Adjusts stamina usage for certain actions.</summary>
   * <param name="__instance">The patched player object.</param>
   * <param name="v">The amount of stamina used.</param>
   */
  // ReSharper disable once InconsistentNaming
  // ReSharper disable once UnusedMember.Local
  private static void Prefix(ref Player __instance, ref float v) {
    
    var player = __instance;

    var callingMethod = new StackFrame(2).GetMethod();

    // Only handle player actions here:
    if (callingMethod.DeclaringType != typeof(Player))
      return;
    
    // Whether the player is noticed by any mob:
    var playerNoticed = player.IsSensed() || player.IsTargeted();

    // Check for all the options this mod alters:
    switch (callingMethod.Name, playerNoticed) {
      
      // Running while being noticed:
      case (nameof(Player.CheckRun), true):
        v *= SimplyStaminaSettings.RunStaminaDrainModifier;
        break;

      // Running while being unnoticed:
      case (nameof(Player.CheckRun), false):
        v *= SimplyStaminaSettings.RunUnnoticedStaminaDrainModifier;
        break;
      
      // Jumping while being noticed:
      case (nameof(Player.OnJump), true):
        v *= SimplyStaminaSettings.JumpStaminaDrainModifier;
        break;
      
      // Jumping while being unnoticed:
      case (nameof(Player.OnJump), false):
        v *= SimplyStaminaSettings.JumpUnnoticedStaminaDrainModifier;
        break;
      
      // Sneaking while being noticed:
      case (nameof(Player.OnSneaking), true):
        v *= SimplyStaminaSettings.SneakStaminaDrainModifier;
        break;
      
      // Sneaking while being unnoticed:
      case (nameof(Player.OnSneaking), false):
        v *= SimplyStaminaSettings.SneakUnnoticedStaminaDrainModifier;
        break;
      
      // Using a tool:
      case (nameof(Player.Repair) or nameof(Player.UpdatePlacement), _):
        
        // The name of the item the player used:
        var usedItemName = player.GetRightItem()?.m_shared?.m_name;

        if (
          (usedItemName is "$item_hammer" && SimplyStaminaSettings.DisableHammerStaminaUsage)
          || (usedItemName is "$item_hoe" && SimplyStaminaSettings.DisableHoeStaminaUsage)
          || (usedItemName is "$item_cultivator" && SimplyStaminaSettings.DisableCultivatorStaminaUsage)
        ) {
          v = 0.0f;
        }
        break;
      
    }
  }
    
}
