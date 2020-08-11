# DocRework

DocRework is an SCP:SL plugin that gives our good old Doctor - SCP-049 - a well deserved buff and more effective tools to cure people from the pestilence. **Reaching a specified number of cures, the Doctor becomes euphoric and spawns an aura around himself that heals SCP-049-2s every few seconds.**

As this is a first release of the plugin, expect many changes and additions in the future.

## Requirements
- This plugin uses [EXILED](https://github.com/galaxy119/EXILED/).

Note: **DocRework 1.0 requires Exiled 2.0+ and SCP:SL 10.0+!**

## Releases
You can find the latest release [here](https://github.com/rby-blackruby/DocRework/releases).

## Configs (SCP:SL 10.0+)
| Config option | Value type | Default value | Description |
| --- | --- | --- | --- |
| `IsEnabled` | bool | true | Enables the DocRework plugin. Set it to false to disable it. |
| `Start` | int | 3 | Set the minimum cure amount required for the buff area to spawn. |
| `DocMessage` | string | '<color=red>Your passive ability is now activated.\nYou now heal zombies around you every 5 seconds.</color>' | Message sent to SCP-049 upon reaching the minimum cures amount required. |
| `HealRadius` | float | 2.6f | 049's area's healing radius. Don't set it to 0 or below! |
| `HealAmount` | float | 15.0f | The amount of HP the Doc heals their zombies. Don't set it to 0 or below! |

## Thank you!

Thank you for being interested in this plugin and I wish you a great time using it! If you have any ideas/problems feel free to contact me on discord: `blackruby#0001`
