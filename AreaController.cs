using System.Collections.Generic;
using System.Linq;
using Hints;
using MEC;
using UnityEngine;
using Player = Exiled.API.Features.Player;

namespace DocRework
{
    public class AreaController
    {

        /*
         * TO DO: Instead of flat hp / heal, make it missing health percentage.
         * The more the doctor kills the higher the percentage is.
         * Implement level system.
        */

        public static int CureCounter = 0;
        // public static int Level = 1;
        private static float Radius = DocRework.config.HealRadius;
        private static float HealAmount = DocRework.config.HealAmount;

        public static IEnumerator<float> EngageBuff()
        {
            while (true)
            {
                // Check EVERY zombies' position for EVERY Doctor.
                foreach (Player D in Player.List.Where(r => r.Role == RoleType.Scp049))
                    foreach (Player Z in Player.List.Where(r => r.Role == RoleType.Scp0492))

                        // Check if a Zombie (Z) is inside of a aura drawn around the Doctor (D)
                        if (Vector3.Distance(D.Position, Z.Position) <= Radius)
                            ApplyHealEffects(Z, HealAmount);

                yield return Timing.WaitForSeconds(5f);
            }
        }

        // Do the healing bit and handle broadcasts to zombies.
        private static void ApplyHealEffects(Player p, float h)
        {
            float HpGiven = 0;
            if (p.Health + h > p.MaxHealth)
            {
                HpGiven = p.MaxHealth - p.Health;
                p.Health = p.MaxHealth;
            } 
            else
            {
                HpGiven = h;
                p.Health += h;
            }

            p.HintDisplay.Show(new TextHint($"<color=red>+{HpGiven} HP</color>", new HintParameter[] { new StringHintParameter("") }, null, 2f));
        }
    }
}
