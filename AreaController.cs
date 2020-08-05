using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using MEC;
using UnityEngine;
using Player = Exiled.API.Features.Player;

namespace DocRework
{
    public class AreaController
    {
        public static int CureCounter = 0;
        public static IEnumerable<Player> Doctors = Player.List.Where(r => r.Role == RoleType.Scp049);
        private static IEnumerable<Player> Zombies = Player.List.Where(r => r.Role == RoleType.Scp0492);
        private static float Radius = DocRework.config.HealRadius;
        private static string HealMessage = DocRework.config.ZombieMessage;
        private static float HealAmount = DocRework.config.HealAmount;

        public static IEnumerator<float> EngageBuff()
        {
            for (; ; )
            {
                // Check EVERY zombies' position for EVERY Doctor.
                foreach (Player D in Doctors)
                    foreach (Player Z in Zombies)

                        // Check if a Zombie (Z) is inside of a circle drawn around the Doctor (D)
                        if (IsInsideArea(D.Position.x, D.Position.z, Z.Position.x, Z.Position.z, Radius))
                            ApplyHealEffects(Z, HealAmount, HealMessage);

                yield return Timing.WaitForSeconds(5f);
            }
        }

        // Check if the Zombie's coordinates are inside the Doctor's circle.
        private static bool IsInsideArea(float d_x, float d_z, float z_x, float z_z, float radius)
        {
            return Math.Pow(d_x - z_x, 2) + Math.Pow(d_z - z_z,2) < Math.Pow(radius, 2);
        }

        // Do the healing bit and handle broadcasts to zombies.
        private static void ApplyHealEffects(Player p, float h, string m)
        {
            if(p.Health + h > p.MaxHealth)
            {
                p.Health = p.MaxHealth;
                return;
            }

            p.Health += h;
            p.ClearBroadcasts();
            p.Broadcast(3, m, Broadcast.BroadcastFlags.Normal);
        }
    }
}
