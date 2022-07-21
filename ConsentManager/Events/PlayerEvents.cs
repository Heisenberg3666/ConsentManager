using ConsentManager.API;
using ConsentManager.Configs;
using Exiled.Events.EventArgs;
using Exiled.Events.Handlers;

namespace ConsentManager.Events
{
    internal class PlayerEvents
    {
        private Translation _translation;
        private ConsentManagerApi _api;

        public PlayerEvents(Translation translation, ConsentManagerApi api)
        {
            _translation = translation;
            _api = api;
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
            if (!e.Player.DoNotTrack)
                ConsentManager.Instance._consented.Add(e.Player.Id);

            if (!_api.HasPlayerGivenConsent(e.Player))
                e.Player.OpenReportWindow(_translation.PopupMessage);
        }
    }
}
