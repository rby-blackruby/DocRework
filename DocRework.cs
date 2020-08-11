using Exiled.API.Features;
using Doc = Exiled.Events.Handlers.Scp049;
using Srv = Exiled.Events.Handlers.Server;

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

            EventHandler = new EventHandler();
            Doc.FinishingRecall += EventHandler.OnFinishingRecall;
            Srv.RoundStarted += EventHandler.OnRoundStart;
        }

        public override void OnDisabled()
        {   
            // Unsubscribe events
            Doc.FinishingRecall -= EventHandler.OnFinishingRecall;
            Srv.RoundStarted -= EventHandler.OnRoundStart;
            EventHandler = null;
        }
    }
}
