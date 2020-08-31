using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Hints;
using MEC;
using UnityEngine;
using Player = Exiled.API.Features.Player;

namespace DocRework
{
    public class SCP049AbilityController
    {

        public static uint CureCounter =                        0;
        public static ushort AbilityCooldown;
        private static float Radius =                           DocRework.config.HealRadius;
        private static float HealAmountFlat =                   DocRework.config.HealAmountFlat;
        public static float HealAmountPercentage =              DocRework.config.ZomHealAmountPercentage;
        private static byte HealType =                          DocRework.config.HealType;

        public static IEnumerator<float> EngageBuff()
        {
            while (true)
            {
                // Check EVERY zombies' position for EVERY Doctor.
                foreach (Player D in Player.List.Where(r => r.Role == RoleType.Scp049))
                    foreach (Player Z in Player.List.Where(r => r.Role == RoleType.Scp0492))

                        // Check if a Zombie (Z) is inside of a aura drawn around the Doctor (D)
                        if (Vector3.Distance(D.Position, Z.Position) <= Radius)
                            ApplyHeal(HealType, Z, HealAmountFlat, HealAmountPercentage);


                yield return Timing.WaitForSeconds(5f);
            }
        }

        private static void ApplyHeal(byte type, Player p, float flat, float multiplier)
        {
            float HpGiven;
            bool CanDisplay = true;
            float MissingHP = p.MaxHealth - p.Health;

            if (p.Health == p.MaxHealth)
                CanDisplay = false;

            // Flat hp
            if (type == 0)
            {
                if (p.Health + flat > p.MaxHealth)
                {
                    HpGiven = p.MaxHealth - p.Health;
                    p.Health = p.MaxHealth;
                }
                else
                {
                    HpGiven = flat;
                    p.Health += flat;
                }

            }

            // Percentage HP
            else
            {
                if (p.Health + MissingHP * multiplier > p.MaxHealth)
                {
                    HpGiven = p.MaxHealth - p.Health;
                    p.Health = p.MaxHealth;
                }
                else
                {
                    HpGiven = MissingHP * multiplier;
                    p.Health += MissingHP * multiplier;
                }
            }

            // Sent Zombies notification that they got healed.
            if(CanDisplay)
                p.HintDisplay.Show(new TextHint($"<color=red>+{HpGiven} HP</color>", new HintParameter[] { new StringHintParameter("") }, null, 2f));
        }

        public static void ApplySelfHeal(Player p, float missing)
        {
            float MissingHP = p.MaxHealth - p.Health;
            if (p.Health + MissingHP * missing > p.MaxHealth) p.Health = p.MaxHealth; else p.Health += MissingHP * missing;
        }

        public static void CallZombieReinforcement(Player p, ushort cd, SendingConsoleCommandEventArgs ev)
        {
            // List of spectators to randomly choose from later
            List<Player> list = Player.List.Where(r => r.Role == RoleType.Spectator).ToList();

            // Only 049 is allowed to use this command
            if (p.Role != RoleType.Scp049)
            {
                ev.ReturnMessage = DocRework.config.Translation_Active_PermissionDenied;
                return;
            }

            if (CureCounter < DocRework.config.MinCures)
            {
                ev.ReturnMessage = DocRework.config.Translation_Active_NotEnoughRevives;
                return;
            }

            // Pretty self-explanatory i think
            if (cd > 0)
            {
                ev.ReturnMessage = DocRework.config.Translation_Active_OnCooldown + cd;
                return;
            }

            // If the list is empty it means no spectators can be chosen.
            if (list.IsEmpty())
            {
                ev.ReturnMessage = DocRework.config.Translation_Active_NoSpectators;
                return;
            }

            // Get a random player from the spectator list and spawn it as 049-2 then tp it to doc.
            var index =         0;
            index +=            new System.Random().Next(list.Count);
            var selected =      list[index];

            selected.SetRole(RoleType.Scp0492);
            selected.Health = selected.MaxHealth;

            Timing.CallDelayed(0.5f, () =>
            {
                selected.Position = new Vector3(p.Position.x, p.Position.y, p.Position.z);
            });

            index = 0;

            AbilityCooldown = DocRework.config.Cooldown;
            Timing.RunCoroutine(StartCooldownTimer(), "SCP049_Active_Cooldown");
        }

        public static IEnumerator<float> StartCooldownTimer()
        {
            while(AbilityCooldown != 0)
            {
                AbilityCooldown--;
                yield return Timing.WaitForSeconds(1f);
            }

            // Notify the Doc that the ability's cd has expired.
            foreach(Player D in Player.List.Where(p => p.Role == RoleType.Scp049))
                D.HintDisplay.Show(new TextHint(DocRework.config.Translation_Active_ReadyNotification, new HintParameter[] { new StringHintParameter("") }, null, 5f));

            // Kill it just for sure.
            Timing.KillCoroutines("SCP049_Active_Cooldown");
        }
    }
}
