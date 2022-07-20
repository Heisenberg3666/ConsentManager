using Exiled.API.Features;

namespace ConsentManager.API
{
    public static class Extensions
    {
        public static bool GivenConsent(this Player player) => ConsentManagerApi.HasGivenConsent(player);

        internal static void AddConsent(this Player player) => ConsentManagerApi.AddConsent(player);
        internal static void RemoveConsent(this Player player) => ConsentManagerApi.RemoveConsent(player);
        internal static void UpdateConsentedPlayers(this Player player) => ConsentManagerApi.UpdateConsentedPlayers(player);
    }
}
