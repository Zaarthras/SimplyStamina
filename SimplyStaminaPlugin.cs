using BepInEx;
using HarmonyLib;
using ServerSync;

namespace SimplyStamina;

/**
 * <summary>Provides logic to adjust stamina drain.</summary>
 */
[BepInPlugin(ModId, DisplayName, Version)]
public class SimplyStaminaPlugin : BaseUnityPlugin {

  /**
   * <summary>The unique ID for this mod.</summary>
   */
  private const string ModId = "zaarthras.simply_stamina";

  /**
   * <summary>The display name for this mod.</summary>
   */
  private const string DisplayName = "Simply Stamina";

  /**
   * <summary>The current version of this mod.</summary>
   */
  private const string Version = "1.1.0";
  
  /**
   * <summary>Plugin startup logic.</summary>
   */
  private void Awake() {
    var configSync = new ConfigSync(ModId) {
      DisplayName = DisplayName,
      CurrentVersion = Version,
      MinimumRequiredVersion = Version,
    };
    SimplyStaminaSettings.Init(Config, configSync);
    new Harmony(ModId).PatchAll();
  }
  
}
