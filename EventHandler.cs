using Exiled.Events.EventArgs;
using Player = Exiled.API.Features.Player;
using MEC;

namespace DocRework

{
    public class EventHandler
    {
        static CoroutineHandle Handler;
        private static int MinCure = DocRework.config.Start;
        public static void OnFinishingRecall(FinishingRecallEventArgs ev)
        {
            // Counter for every player the Doctor has cured.
            AreaController.CureCounter++;
            if (AreaController.CureCounter == MinCure)
            {
                // Notify the Doctor that the buff is now active.
                foreach(Player D in AreaController.Doctors)
                {
                    D.ClearBroadcasts();
                    D.Broadcast(5, DocRework.config.DocMessage, Broadcast.BroadcastFlags.Normal);
                }

                // Run the actual EngageBuff corouting every 5 seconds.
                Handler = Timing.RunCoroutine(AreaController.EngageBuff());
            }
        }

        public static void OnRoundEnd(RoundEndedEventArgs ev)
        {
            Timing.KillCoroutines(Handler);
        }
    }
}
