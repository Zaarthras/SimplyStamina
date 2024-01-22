# Simply Stamina - Valheim Mod

## Description

**Simply Stamina** is a Valheim mod that lets you adjust stamina drain for toll usage and movement actions.
Currently, it offers the following modifications:
- Disable stamina usage for the *hammer*, *hoe* and *cultivator*.
- Decrease stamina drain for *running*, *jumping* and *sneaking*.
- Adjust stamina drain for those movement actions when you're not noticed by any passive or aggressive mob.
- Disable stamina drain for moving while encumbered. You will still not be able to run or jump while being encumbered.

All aspects can be individually adjusted in the configuration.

## Configuration

To tailor the mod to your preferred gameplay style, you can adjust the following settings in the configuration:

### Tools

- `Disable Hammer Stamina Usage`
    - Disable stamina usage for using the hammer to build, repair or destroy pieces.
    - Default: `true` (Disables the stamina usage)
- `Disable Hoe Stamina Usage`
    - Disable stamina usage for using the hoe.
    - Default: `true` (Disables the stamina usage)
- `Disable Cultivator Stamina Usage`
    - Disable stamina usage for using the cultivator.
    - Default: `true` (Disables the stamina usage)

### Movement

All settings here except for `Disable Encumbered Stamina Drain` are percentage modifiers. Setting them to `0` will disable the stamina usage, a value of `100` will be the vanilla experience.
Negative values or values above `100` are not allowed.

- `Disable Encumbered Stamina Drain`
    - Disable the stamina drain for moving while being encumbered. You will still not be able to run or jump while being encumbered.
    - Default: `true` (Disables the stamina drain)
- `Run Stamina Drain Modifier`
    - Percentage modifier to decrease stamina drain for running.
    - Default: `75`
- `Run Unnoticed Stamina Drain Modifier`
    - Percentage modifier to decrease stamina drain for running while no mob has noticed the player.
    - Default: `50`
- `Jump Stamina Drain Modifier`
    - Percentage modifier to decrease stamina drain for jumping.
    - Default: `100`
- `Jump Unnoticed Stamina Drain Modifier`
    - Percentage modifier to decrease stamina drain for running while no mob has noticed the player.
    - Default: `100`
- `Sneak Stamina Drain Modifier`
    - Percentage modifier to decrease stamina drain for sneaking.
    - Default: `100`
- `Sneak Unnoticed Stamina Drain Modifier`
    - Percentage modifier to decrease stamina drain for sneaking while no mob has noticed the player.
    - Default: `100`

## Compatibility

Simply Stamina is designed to be compatible with most Valheim mods. However, it may exhibit unexpected behavior if used in conjunction with other mods that alter stamina mechanics. Exercise caution when combining multiple stamina-altering mods.