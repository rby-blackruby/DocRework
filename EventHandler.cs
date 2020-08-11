using System.Linq;
using Exiled.Events.EventArgs;
using Player = Exiled.API.Features.Player;
using MEC;
using Hints;

namespace DocRework

{
    public class EventHandler
    {
        static CoroutineHandle Handler;
        public static void OnFinishingRecall(FinishingRecallEventArgs ev)
        {
            // Check if round is still in progress
            if (!RoundSummary.RoundInProgress()) return;

            // Counter for every player the Doctor has cured.
            AreaController.CureCounter++;

            if (AreaController.CureCounter == DocRework.config.Start)
            {
                // Notify the Doctor that the buff is now active.
                foreach(Player D in Player.List.Where(r => r.Role == RoleType.Scp049))
                    D.HintDisplay.Show(new TextHint(DocRework.config.DocMessage, new HintParameter[] { new StringHintParameter("") }, null, 5f));

                // Run the actual EngageBuff corouting every 5 seconds.
                Handler = Timing.RunCoroutine(AreaController.EngageBuff());
            } 
        }

        public static void OnRoundStart()
        {
            // Kill the EngageBuff coroutine once the roudn starts
            if(Handler.IsValid) 
                Timing.KillCoroutines(Handler);

            // Reset values to their default
            AreaController.CureCounter = 0;
            // AreaController.Level = 1;
        }
    }
}
