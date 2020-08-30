using System.ComponentModel;
using Exiled.API.Interfaces;

namespace DocRework
{
    public class Config : IConfig
    {
        private static ushort cd = SCP049AbilityController.AbilityCooldown;

        /*            
         * PLUGIN
        */

        [Description("Enable or disable DocRework's mechanics")]
        public bool IsEnabled { get; set; } = true;

        /*
         *  DOCTOR PASSIVE
        */

        [Description("DOCTOR PASSIVE\nAllow SCP-049 to be healed for a percentage of it's missing health every player revival")]
        public bool AllowDocSelfHeal { get; set; } = true;

        [Description("Set the minimum cure amount for the buff area to kick in")]
        public int MinCures { get; set; } = 3;

        [Description("Change between 049's arua's heal type: 0 is for flat HP, 1 is for missing % HP")]
        public byte HealType { get; set; } = 0;

        [Description("Size of 049's healing radius")]
        public float HealRadius { get; set; } = 2.6f;

        [Description("The amount of HP the Doc heals their Zombies")]
        public float HealAmountFlat { get; set; } = 15.0f;

        [Description("The base amount of missing % HP the Doc heals their Zombies at the start of their buff")]
        public float ZomHealAmountPercentage { get; set; } = 10.0f;

        [Description("Multiplier for the HealAmountPercentage value every time a Doctor revives someone")]
        public float HealPercentageMultiplier { get; set; } = 1.3f;

        [Description("Percentage of SCP-049's missing health to be healed")]
        public float DocMissingHealthPercentage { get; set; } = 15.0f;

        /*
         * DOCTOR ACTIVE
        */

        [Description("DOCTOR ACTIVE\nCooldown for SCP049 active ability")]
        public ushort Cooldown { get; set; } = 180;

        /*
         * ZOMBIE PASSIVE
        */

        [Description("ZOMBIE PASSIVE\nAllow SCP-049-2 to damage everyone around upon hitting an enemy target")]
        public bool AllowZombieAOE { get; set; } = true;

        [Description("Amount of health each person in 049-2's range loses by 049-2's AOE attack")]
        public float ZombieAOEDamage { get; set; } = 15.0f;

        /*
         * TRANSLATIONS 
        */

        [Description("TRANSLATIONS\nMessage sent to SCP-049 upon reaching the minimum cures amount required")]
        public string Passive_ActivationMessage { get; set; } = "<color=red>Your passive ability is now activated.\nYou now heal zombies around you every 5 seconds.</color>";

        [Description("Message sent when you try to execute the .cr command when you're not a doctor")]
        public string Active_PermissionDenied { get; set; } = "You are not allowed to use this command!";

        [Description("Message sent when you try to execute the .cr command but you don't yet have the min required revives")]
        public string Active_NotEnoughRevives { get; set; } = "You don't have enough revives to use this ability!";

        [Description("Message sent when you try to execute the .cr command while it's on cooldown")]
        public string Active_OnCooldown { get; set; } = $"You must wait {cd} seconds before using this ability!";

        [Description("Message send when there are no spectators to spawn")]
        public string Active_NoSpectators { get; set; } = "Sorry, but we were unable to find any spectators for you. :(";

        [Description("Hint displayed when the .cr ability's cooldown has expired")]
        public string Active_ReadyNotification { get; set; } = "<color=green>You can now use your active ability.\nUse .cr in your console to spawn a zombie from the spectators.</color>";
    }
}
