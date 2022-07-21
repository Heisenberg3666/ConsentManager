using ConsentManager.API;
using ConsentManager.Configs;
using Exiled.Events.EventArgs;
using Exiled.Events.Handlers;
using System;

namespace ConsentManager.Events
{
    internal class PlayerEvents
    {
        private Translation _translation;
        private Guid _apiKey;

        public PlayerEvents(Translation translation, Guid apiKey)
        {
            _translation = translation;
            _apiKey = apiKey;
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

            if (!ConsentManagerApi.HasPlayerGivenConsent(e.Player, _apiKey))
                e.Player.OpenReportWindow(_translation.PopupMessage);
        }
    }
}
