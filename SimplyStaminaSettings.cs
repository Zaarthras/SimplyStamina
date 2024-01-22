using System.Collections.Generic;
using BepInEx.Configuration;
using BepInEx.Logging;
using JetBrains.Annotations;
using ServerSync;

namespace SimplyStamina; 

/**
 * <summary>Configuration settings for the simply stamina mod.</summary>
 */
public static class SimplyStaminaSettings {
  
  /**
   * <summary>The config to bind settings to.</summary>
   */
  private static ConfigFile? config;

  /**
   * <summary>The config sync instance.</summary>
   */
  private static ConfigSync? configSync;

  #region Synced Config Entries
  
  /**
   * <summary>Synced config entry that defines if stamina drain for using the hammer is disabled.</summary>
   */
  private static SyncedConfigEntry<bool>? disableHammerStaminaUsageConfigEntry;

  /**
   * <summary>Synced config entry that defines if stamina drain for using the hoe is disabled.</summary>
   */
  private static SyncedConfigEntry<bool>? disableHoeStaminaUsageConfigEntry;

  /**
   * <summary>Synced config entry that defines if stamina drain for using the cultivator is disabled.</summary>
   */
  private static SyncedConfigEntry<bool>? disableCultivatorStaminaUsageConfigEntry;

  /**
   * <summary>Synced config entry that defines if stamina drain for moving while encumbered is disabled.</summary>
   */
  private static SyncedConfigEntry<bool>? disableEncumberedStaminaDrainConfigEntry;

  /**
   * <summary>Synced config entry of the multiplication modifier for stamina drain for running.</summary>
   */
  private static SyncedConfigEntry<float>? runStaminaDrainModifierConfigEntry;

  /**
   * <summary>
   * Synced config entry of the multiplication modifier for stamina drain for running while not being noticed by mobs.
   * </summary>
   */
  private static SyncedConfigEntry<float>? runUnnoticedStaminaDrainModifierConfigEntry;

  /**
   * <summary>Synced config entry of the multiplication modifier for stamina drain for jumping.</summary>
   */
  private static SyncedConfigEntry<float>? jumpStaminaDrainModifierConfigEntry;

  /**
   * <summary>
   * Synced config entry of the multiplication modifier for stamina drain for jumping while not being noticed by mobs.
   * </summary>
   */
  private static SyncedConfigEntry<float>? jumpUnnoticedStaminaDrainModifierConfigEntry;

  /**
   * <summary>Synced config entry of the multiplication modifier for stamina drain for sneaking.</summary>
   */
  private static SyncedConfigEntry<float>? sneakStaminaDrainModifierConfigEntry;

  /**
   * <summary>
   * Synced config entry of the multiplication modifier for stamina drain for sneaking while not being noticed by mobs.
   * </summary>
   */
  private static SyncedConfigEntry<float>? sneakUnnoticedStaminaDrainModifierConfigEntry;

  #endregion Synced Config Entries

  #region Public Getters

  /**
   * <summary>Defines if stamina drain for using the hammer is disabled.</summary>
   */
  public static bool DisableHammerStaminaUsage => disableHammerStaminaUsageConfigEntry?.Value ?? false;

  /**
   * <summary>Defines if stamina drain for using the hoe is disabled.</summary>
   */
  public static bool DisableHoeStaminaUsage => disableHoeStaminaUsageConfigEntry?.Value ?? false;

  /**
   * <summary>Defines if stamina drain for using the cultivator is disabled.</summary>
   */
  public static bool DisableCultivatorStaminaUsage => disableCultivatorStaminaUsageConfigEntry?.Value ?? false;

  /**
   * <summary>Defines if stamina drain for moving while encumbered is disabled.</summary>
   */
  public static bool DisableEncumberedStaminaDrain => disableEncumberedStaminaDrainConfigEntry?.Value ?? false;

  /**
   * <summary>Multiplication modifier for stamina drain for running.</summary>
   */
  public static float RunStaminaDrainModifier => (runStaminaDrainModifierConfigEntry?.Value ?? 100.0f) / 100.0f;
  
  /**
   * <summary>Multiplication modifier for stamina drain for running while not being noticed by mobs.</summary>
   */
  public static float RunUnnoticedStaminaDrainModifier => (runUnnoticedStaminaDrainModifierConfigEntry?.Value ?? 100.0f) / 100.0f;

  /**
   * <summary>Multiplication modifier for stamina drain for jumping.</summary>
   */
  public static float JumpStaminaDrainModifier => (jumpStaminaDrainModifierConfigEntry?.Value ?? 100.0f) / 100.0f;
  
  /**
   * <summary>Multiplication modifier for stamina drain for jumping while not being noticed by mobs.</summary>
   */
  public static float JumpUnnoticedStaminaDrainModifier => (jumpUnnoticedStaminaDrainModifierConfigEntry?.Value ?? 100.0f) / 100.0f;

  /**
   * <summary>Multiplication modifier for stamina drain for sneaking.</summary>
   */
  public static float SneakStaminaDrainModifier => (sneakStaminaDrainModifierConfigEntry?.Value ?? 100.0f) / 100.0f;
  
  /**
   * <summary>Multiplication modifier for stamina drain for sneaking while not being noticed by mobs.</summary>
   */
  public static float SneakUnnoticedStaminaDrainModifier => (sneakUnnoticedStaminaDrainModifierConfigEntry?.Value ?? 100.0f) / 100.0f;

  #endregion Public Getters
  
  /**
   * <summary>Initiates configuration settings.</summary>
   * <param name="currentConfig">The config to bind the settings to.</param>
   * <param name="currentConfigSync">The config sync instance.</param>
   */
  public static void Init(ConfigFile currentConfig, ConfigSync currentConfigSync) {

    config = currentConfig;
    configSync = currentConfigSync;

    disableHammerStaminaUsageConfigEntry = BindBinaryConfigurationSetting(
      "Tools",
      "Disable Hammer Stamina Usage",
      true,
      "Disable stamina usage for using the hammer to build, repair or destroy pieces."
    );

    disableHoeStaminaUsageConfigEntry = BindBinaryConfigurationSetting(
      "Tools",
      "Disable Hoe Stamina Usage",
      true,
      "Disable stamina usage for using the hoe."
    );
    
    disableCultivatorStaminaUsageConfigEntry = BindBinaryConfigurationSetting(
      "Tools",
      "Disable Cultivator Stamina Usage",
      true,
      "Disable stamina usage for using the cultivator."
    );
    
    disableEncumberedStaminaDrainConfigEntry = BindBinaryConfigurationSetting(
      "Movement", 
      "Disable Encumbered Stamina Drain", 
      true, 
      "Disable the stamina drain for moving while being encumbered. You will still not be able to run or jump while being encumbered."
    );
    
    runStaminaDrainModifierConfigEntry = BindAdjustmentConfigurationSetting(
      "Movement",
      "Run Stamina Drain Modifier",
      75f,
      "Percentage modifier to decrease stamina drain for running."
    );

    runUnnoticedStaminaDrainModifierConfigEntry = BindAdjustmentConfigurationSetting(
      "Movement",
      "Run Unnoticed Stamina Drain Modifier",
      50f,
      "Percentage modifier to decrease stamina drain for running while no mob has noticed the player."
    );

    jumpStaminaDrainModifierConfigEntry = BindAdjustmentConfigurationSetting(
      "Movement",
      "Jump Stamina Drain Modifier",
      100f,
      "Percentage modifier to decrease stamina drain for jumping."
    );

    jumpUnnoticedStaminaDrainModifierConfigEntry = BindAdjustmentConfigurationSetting(
      "Movement",
      "Jump Unnoticed Stamina Drain Modifier",
      100f,
      "Percentage modifier to decrease stamina drain for jumping while no mob has noticed the player."
    );

    sneakStaminaDrainModifierConfigEntry = BindAdjustmentConfigurationSetting(
      "Movement",
      "Sneak Stamina Drain Modifier",
      100f,
      "Percentage modifier to decrease stamina drain for sneaking."
    );

    sneakUnnoticedStaminaDrainModifierConfigEntry = BindAdjustmentConfigurationSetting(
      "Movement",
      "Sneak Unnoticed Stamina Drain Modifier",
      100f,
      "Percentage modifier to decrease stamina drain for sneaking while no mob has noticed the player."
    );
  }

  /**
   * <summary>Binds a binary config setting to the config and synchronizes it.</summary>
   * <param name="section">The configuration section to list this setting in.</param>
   * <param name="key">The config key to bind.</param>
   * <param name="defaultValue">The default value.</param>
   * <param name="description">The config setting's description.</param>
   */
  private static SyncedConfigEntry<bool> BindBinaryConfigurationSetting(string section, string key, bool defaultValue, string description) {

    var entry = config!.Bind(
      section,
      key,
      defaultValue,
      new ConfigDescription(
        description, 
        new AcceptableValueList<bool>(true, false)
      )
    );
    var syncedEntry = configSync!.AddConfigEntry(entry);
    syncedEntry.SynchronizedConfig = true;
    return syncedEntry;
  }
  
  /**
   * <summary>Binds a adjustment config setting to the config and synchronizes it.</summary>
   * <param name="section">The configuration section to list this setting in.</param>
   * <param name="key">The config key to bind.</param>
   * <param name="defaultValue">The default value.</param>
   * <param name="description">The config setting's description.</param>
   */
  private static SyncedConfigEntry<float> BindAdjustmentConfigurationSetting(string section, string key,
    float defaultValue, string description) {

    var entry = config!.Bind(
      section,
      key,
      defaultValue,
      new ConfigDescription(
        description + " Setting this to 0 will disable the stamina usage, a value of 100 will be the default experience.", 
        new AcceptableValueRange<float>(0f, 100f)
      )
    );
    var syncedEntry = configSync!.AddConfigEntry(entry);
    syncedEntry.SynchronizedConfig = true;
    return syncedEntry;
  }

}
