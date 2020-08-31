using System.Linq;
using Exiled.Events.EventArgs;
using Player = Exiled.API.Features.Player;
using MEC;
using Hints;
using UnityEngine;

namespace DocRework

{
    public class EventHandler
    {
        private static bool AllowDocHeal =                      DocRework.config.AllowDocSelfHeal;
        private static float MissingHealthPercentage =          DocRework.config.DocMissingHealthPercentage;
        private static bool EnableZombieAOEDamage =             DocRework.config.AllowZombieAoe;
        private static float ZombieAOEDamage =                  DocRework.config.ZombieAoeDamage;

        public static void OnFinishingRecall(FinishingRecallEventArgs ev)
        {
            // Check if round is still in progress
            if (!RoundSummary.RoundInProgress()) return;

            // Counter for every player the Doctor has cured.
            SCP049AbilityController.CureCounter++;

            if (SCP049AbilityController.CureCounter == DocRework.config.MinCures)
            {
                // Notify the Doctor that the buff is now active.
                foreach (Player D in Player.List.Where(r => r.Role == RoleType.Scp049))
                    D.HintDisplay.Show(new TextHint(DocRework.config.Translation_Passive_ActivationMessage, new HintParameter[] { new StringHintParameter("") }, null, 5f));

                // Run the actual EngageBuff corouting every 5 seconds.
                Timing.RunCoroutine(SCP049AbilityController.EngageBuff(), "SCP049_Passive");
                Timing.RunCoroutine(SCP049AbilityController.StartCooldownTimer(), "SCP049_Active_Cooldown");
            }

            // Increase the percentage zombies get healed for by the HealthPercentageMultiplier if the config option is set to 1 (percentage of missing hp mode)
            if (DocRework.config.HealType == 1 && SCP049AbilityController.CureCounter > DocRework.config.MinCures)
                SCP049AbilityController.HealAmountPercentage *= DocRework.config.HealPercentageMultiplier;

            // Heal the doctor for the configured percentage if it's missing health if the config option for it is set to true
            if(AllowDocHeal) SCP049AbilityController.ApplySelfHeal(ev.Scp049, MissingHealthPercentage);
        }

        public static void OnPlayerHit(HurtingEventArgs ev)
        {
            if (EnableZombieAOEDamage && SCP049AbilityController.CureCounter >= DocRework.config.MinCures)
                SCP0492AbilityController.DealAOEDamage(ev.Attacker, ev.Target, ZombieAOEDamage);
        }

        public static void OnClientCommand(SendingConsoleCommandEventArgs ev)
        {
            ev.Allow = false;
            if (ev.Name.ToLower().Equals("cr"))
                SCP049AbilityController.CallZombieReinforcement(ev.Player, SCP049AbilityController.AbilityCooldown, ev);
        }

        public static void OnRoundStart()
        {
            // Kill the EngageBuff coroutine once the round starts
            try 
            {
                Timing.KillCoroutines("SCP049_Passive");
                Timing.KillCoroutines("SCP049_Active_Cooldown");
            } catch {}

            // Reset values to their default
            SCP049AbilityController.CureCounter =       0;
            SCP049AbilityController.AbilityCooldown =   DocRework.config.Cooldown;
        }
    }
}
