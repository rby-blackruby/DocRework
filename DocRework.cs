using Exiled.API.Features;
using Doc = Exiled.Events.Handlers.Scp049;
using Srv = Exiled.Events.Handlers.Server;
using Ply = Exiled.Events.Handlers.Player;
using MEC;

namespace DocRework
{
    public class DocRework : Plugin<Config>
    {
        public static Config config;
        public EventHandler EventHandler;

        public override void OnEnabled()
        {
            config = Config;

            if(!config.IsEnabled)
            {
                Log.Info("DocRework is currently disabled on this server. To enable it, set IsEnabled to true in your server's configs");
                return;
            }

            Log.Info("DocRework is currently enabled on this server. Thank you for using DocRework. xoxo, blackruby");

            if(config.HealType != 0 && config.HealType != 1)
            {
                config.HealType = 0;
                Log.Info("HealType is defaulted to 0 (Flat HP mode) due to incorrect HealType configuration.");
            }

            EventHandler =                  new EventHandler();
            Doc.FinishingRecall +=          EventHandler.OnFinishingRecall;
            Srv.RoundStarted +=             EventHandler.OnRoundStart;
            Ply.Hurting +=                  EventHandler.OnPlayerHit;
            Srv.SendingConsoleCommand +=    EventHandler.OnClientCommand;
        }

        public override void OnDisabled()
        {
            // Kill the EngageBuff coroutine on plugin reload
            // All the other coroutines should be killed by themselves
            try
            {
                Timing.KillCoroutines("SCP049_Active");
                Timing.KillCoroutines("SCP049_Active_Cooldown");
            } 
            catch {}

            // Unsubscribe events
            Doc.FinishingRecall -=          EventHandler.OnFinishingRecall;
            Srv.RoundStarted -=             EventHandler.OnRoundStart;
            Ply.Hurting -=                  EventHandler.OnPlayerHit;
            Srv.SendingConsoleCommand -=    EventHandler.OnClientCommand;
            EventHandler =                  null;

            // Reset values to their default
            SCP049AbilityController.CureCounter =       0;
            SCP049AbilityController.AbilityCooldown =   config.Cooldown;
        }
    }
}
