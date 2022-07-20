using ConsentManager.API;
using ConsentManager.Configs;
using Exiled.Events.EventArgs;
using Exiled.Events.Handlers;

namespace ConsentManager.Events
{
    internal class PlayerEvents
    {
        private Translation _translation;

        public PlayerEvents(Translation translation)
        {
            _translation = translation;
        }

        public void RegisterEvents()
        {
            Player.Verified += OnVerified;
        }

        public void UnregisterEvents()
        {
            Player.Verified -= OnVerified;
        }

        private void OnVerified(VerifiedEventArgs e)
        {
            e.Player.UpdateConsentedPlayers();

            if (!e.Player.GivenConsent())
                e.Player.SendConsoleMessage(_translation.ConsoleMessage, "green");
        }
    }
}
