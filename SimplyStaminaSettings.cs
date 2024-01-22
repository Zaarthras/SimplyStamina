using BepInEx.Configuration;

namespace SimplyStamina; 

/**
 * <summary>Configuration settings for the simply stamina mod.</summary>
 */
public static class SimplyStaminaSettings {
  
  /**
   * <summary>Defines if stamina drain for using the hammer is disabled.</summary>
   */
  public static bool DisableHammerStaminaUsage { get; private set; }
  
  /**
   * <summary>Defines if stamina drain for using the hoe is disabled.</summary>
   */
  public static bool DisableHoeStaminaUsage { get; private set; }
  
  /**
   * <summary>Defines if stamina drain for using the cultivator is disabled.</summary>
   */
  public static bool DisableCultivatorStaminaUsage { get; private set; }
  
  /**
   * <summary>Defines if stamina drain for moving while encumbered is disabled.</summary>
   */
  public static bool DisableEncumberedStaminaDrain { get; private set; }

  /**
   * <summary>Multiplication modifier for stamina drain for running.</summary>
   */
  public static float RunStaminaDrainModifier { get; private set; } = 1.0f;
  
  /**
   * <summary>Multiplication modifier for stamina drain for running while not being noticed by mobs.</summary>
   */
  public static float RunUnnoticedStaminaDrainModifier { get; private set; } = 1.0f;

  /**
   * <summary>Multiplication modifier for stamina drain for jumping.</summary>
   */
  public static float JumpStaminaDrainModifier { get; private set; } = 1.0f;
  
  /**
   * <summary>Multiplication modifier for stamina drain for jumping while not being noticed by mobs.</summary>
   */
  public static float JumpUnnoticedStaminaDrainModifier { get; private set; } = 1.0f;

  /**
   * <summary>Multiplication modifier for stamina drain for sneaking.</summary>
   */
  public static float SneakStaminaDrainModifier { get; private set; } = 1.0f;
  
  /**
   * <summary>Multiplication modifier for stamina drain for sneaking while not being noticed by mobs.</summary>
   */
  public static float SneakUnnoticedStaminaDrainModifier { get; private set; } = 1.0f;

  /**
   * <summary>Reloads configuration values from the given config file.</summary>
   * <param name="config">The config file to load values from.</param>
   */
  public static void Reload(ConfigFile config) {
    
    DisableHammerStaminaUsage = config.Bind(
      "Tools",
      "Disable Hammer Stamina Usage",
      true,
      new ConfigDescription(
        "Disable stamina usage for using the hammer to build, repair or destroy pieces.", 
        new AcceptableValueList<bool>(true, false)
      )
    ).Value;

    DisableHoeStaminaUsage = config.Bind(
      "Tools",
      "Disable Hoe Stamina Usage",
      true,
      new ConfigDescription(
        "Disable stamina usage for using the hoe.", 
        new AcceptableValueList<bool>(true, false)
      )
    ).Value;
    
    DisableCultivatorStaminaUsage = config.Bind(
      "Tools",
      "Disable Cultivator Stamina Usage",
      true,
      new ConfigDescription(
        "Disable stamina usage for using the cultivator.", 
        new AcceptableValueList<bool>(true, false)
      )
    ).Value;
    
    DisableEncumberedStaminaDrain = config.Bind(
      "Movement", 
      "Disable Encumbered Stamina Drain", 
      true, 
      new ConfigDescription(
        "Disable the stamina drain for moving while being encumbered. You will still not be able to run or jump while being encumbered.", 
        new AcceptableValueList<bool>(true, false)
      )
    ).Value;
    
    RunStaminaDrainModifier = config.Bind(
      "Movement",
      "Run Stamina Drain Modifier",
      75f,
      new ConfigDescription(
        "Percentage modifier to decrease stamina drain for running. Setting this to 0 will disable the stamina usage, a value of 100 will be the vanilla experience.", 
        new AcceptableValueRange<float>(0f, 100f)
      )
    ).Value / 100f;

    RunUnnoticedStaminaDrainModifier = config.Bind(
      "Movement",
      "Run Unnoticed Stamina Drain Modifier",
      50f,
      new ConfigDescription(
        "Percentage modifier to decrease stamina drain for running while no mob has noticed the player. Setting this to 0 will disable the stamina usage, a value of 100 will be the vanilla experience.", 
        new AcceptableValueRange<float>(0f, 100f)
      )
    ).Value / 100f;

    JumpStaminaDrainModifier = config.Bind(
      "Movement",
      "Jump Stamina Drain Modifier",
      100f,
      new ConfigDescription(
        "Percentage modifier to decrease stamina drain for jumping. Setting this to 0 will disable the stamina usage, a value of 100 will be the vanilla experience.", 
        new AcceptableValueRange<float>(0f, 100f)
      )
    ).Value / 100.0f;

    JumpUnnoticedStaminaDrainModifier = config.Bind(
      "Movement",
      "Jump Unnoticed Stamina Drain Modifier",
      100f,
      new ConfigDescription(
        "Percentage modifier to decrease stamina drain for jumping while no mob has noticed the player. Setting this to 0 will disable the stamina usage, a value of 100 will be the vanilla experience.", 
        new AcceptableValueRange<float>(0f, 100f)
      )
    ).Value / 100f;

    SneakStaminaDrainModifier = config.Bind(
      "Movement",
      "Sneak Stamina Drain Modifier",
      100f,
      new ConfigDescription(
        "Percentage modifier to decrease stamina drain for sneaking. Setting this to 0 will disable the stamina usage, a value of 100 will be the vanilla experience.", 
        new AcceptableValueRange<float>(0f, 100f)
      )
    ).Value / 100f;

    SneakUnnoticedStaminaDrainModifier = config.Bind(
      "Movement",
      "Sneak Unnoticed Stamina Drain Modifier",
      100f,
      new ConfigDescription(
        "Percentage modifier to decrease stamina drain for sneaking while no mob has noticed the player. Setting this to 0 will disable the stamina usage, a value of 100 will be the vanilla experience.", 
        new AcceptableValueRange<float>(0f, 100f)
      )
    ).Value / 100f;
  }
  
}
