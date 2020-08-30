# DocRework

DocRework is an SCP:SL plugin that gives our good old Doctor - SCP-049 - a well deserved rework and more effective tools to cure people from the pestilence. **Reaching a specified number of cures, the Doctor becomes euphoric and spawns an aura around himself that heals SCP-049-2s every few seconds. Having the ability to summon SCP-049-2s from the spectators, the army of Zombies should grow exponentially.**
Promoting teamwork and providing healthier gameplay, playing as SCP-049 and SCP-049-2 should be more fun than ever!

## Features
- After SCP-049 reaching a specified minimum amount of revives, a healing aura spawns around himself that can either heal SCP-049-2's nearby for a flat amount of hp, or a percentage of their missing health.
- Using the .cr command in your player console (~) you can summon an SCP-049-2 from the spectators. (Has a cooldown obv.)*
- Upon reviving a player, SCP-049 gains a percentage of it's missing health back.*
- SCP-049-2s now deal AOE damage, making them very effective in crowded combats.*
- Translation options.

*Only available after reaching the minimum required revives.

## Requirements
- This plugin uses [EXILED](https://github.com/galaxy119/EXILED/).

Note: **DocRework 1.0+ requires Exiled 2.0+ and SCP:SL 10.0+!**

## Releases
You can find the latest release [here](https://github.com/rby-blackruby/DocRework/releases).

## Configs (SCP:SL 10.0+)
| Config option | Value type | Default value | Description |
| --- | --- | --- | --- |
| `IsEnabled` | bool | true | Enables the DocRework plugin. Set it to false to disable it. |
| `AllowDocSelfHeal` | bool | true | Allow SCP-049 to be healed for a percentage of it's missing health every player revival. |
| `MinCures` | int | 3 | Minimum cure amount for the buff area to kick in. |
| `HealType`| byte | 0 | Change between 049's arua's heal type: 0 is for flat HP, 1 is for missing % HP. |
| `HealRadius` | float | 2.6f | 049's area's healing radius. Don't set it to 0 or below! |
| `HealAmountFlat` | float | 15.0f | The amount of flat HP the Doc heals their zombies. Don't set it to 0 or below! |
|`ZomHealAmountPercentage` | float | 10.0f | The base amount of missing % HP the Doc heals their Zombies at the start of their buff. |
| `HealPercentageMultiplier` | float | 1.3f | Multiplier for the ZomHealAmountPercentage value every time a Doctor revives someone. |
| `DocMissingHealthPercentage` | float | 15.0f | Percentage of SCP-049's missing health to be healed. |
| `Cooldown` | ushort | 180 | Cooldown for SCP-049 active ability. |
| `AllowZombieAOE` | bool | true | Allow SCP-049-2 to damage everyone around upon hitting an enemy target. |
| `ZombieAOEDamage` | float | 15.0f | Amount of health each player in 049-2's range loses by 049-2's AOE attack. |

`For translations, please check out the TRANSLATIONS section of the configs!`

## Thank you!

Thank you for being interested in this plugin and I wish you a great time using it! If you have any ideas/problems feel free to contact me on discord: `blackruby#0001`
