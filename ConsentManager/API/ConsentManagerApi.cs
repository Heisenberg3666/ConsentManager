using ConsentManager.API.Entities;
using Exiled.API.Features;
using LiteDB;
using System.Collections.Generic;

namespace ConsentManager.API
{
    public static class ConsentManagerApi
    {
        private static LiteDatabase _database = ConsentManager.Instance._database;
        private static List<int> _consented = new List<int>();

        /// <summary>
        /// This checks if the player has given consent for the server to bypass <see cref="Player.DoNotTrack"/>.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static bool HasGivenConsent(Player player) => _consented.Contains(player.Id);

        internal static void AddConsent(Player player)
        {
            _database.GetCollection<ConsentedPlayer>().Insert(new ConsentedPlayer(player.UserId));
            _consented.Add(player.Id);
        }

        internal static void RemoveConsent(Player player)
        {
            _database.GetCollection<ConsentedPlayer>().Delete(player.UserId);
            _consented.Remove(player.Id);
        }

        internal static void UpdateConsentedPlayers(Player player)
        {
            if (ExistsInDatabase(player)
                || !player.DoNotTrack)
            {
                _consented.Add(player.Id);
            }
            else if (_consented.Contains(player.Id))
            {
                _database.GetCollection<ConsentedPlayer>().Delete(player.UserId);
            }
        }

        private static bool ExistsInDatabase(Player player)
        {
            return _database.GetCollection<ConsentedPlayer>().FindById(player.UserId) != null;
        }
    }
}
