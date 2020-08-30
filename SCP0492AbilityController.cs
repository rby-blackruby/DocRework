using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DocRework
{
    public class SCP0492AbilityController
    {
        public static void DealAOEDamage(Player A, Player T, float AOEDamage)
        {

            // Check if the attacker is a zombie and if the target is not an scp
            if (A.Role != RoleType.Scp0492 || T.Team == Team.SCP)
            {
                return;
            }

            foreach (Player P in Player.List.Where(r => r.Team != Team.SCP && r != A && !r.IsGodModeEnabled))
            {
                if (Vector3.Distance(A.Position, P.Position) > 1.65f)
                {
                    return;
                }

                if (P.Health - AOEDamage > 0)
                {
                    P.Health -= AOEDamage;
                }
                else
                {
                    P.Health = 0;
                    P.Kill(DamageTypes.Scp0492);
                }
            }
        }
    }
}
