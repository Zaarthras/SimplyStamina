using BepInEx;
using HarmonyLib;

namespace SimplyStamina;

/**
 * <summary>Provides logic to adjust stamina drain.</summary>
 */
[BepInPlugin(ModId, "Simply Stamina", "1.0.1.0")]
public class SimplyStaminaPlugin : BaseUnityPlugin {

  /**
   * <summary>The unique ID for this mod.</summary>
   */
  private const string ModId = "zaarthras.simply_stamina";

  /**
   * <summary>Plugin startup logic.</summary>
   */
  private void Awake() {
    SimplyStaminaSettings.Reload(Config);
    new Harmony(ModId).PatchAll();
  }
  
}
