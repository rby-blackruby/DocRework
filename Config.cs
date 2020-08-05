using System.ComponentModel;
using Exiled.API.Interfaces;

namespace DocRework
{
    public class Config : IConfig
    {
        [Description("Enable or disable DocRework's mechanics")]
        public bool IsEnabled { get; set; } = true;

        [Description("Set the minimum cure amount for the buff area to kick in")]
        public int Start { get; set; } = 3;

        [Description("Message sent to SCP-049 upon reaching the minimum cures amount required")]
        public string DocMessage { get; set; } = "You now have an area that heals zombies in your area every few seconds!";

        [Description("Message sent to Zombies upon being healed by the doc")]
        public string ZombieMessage { get; set; } = "You were healed by your Doc.";

        [Description("049's healing radius")]
        public float HealRadius { get; set; } = 2.6f;

        [Description("The amount of HP the Doc heals their zombies")]
        public float HealAmount { get; set; } = 15.0f;
    }
}
