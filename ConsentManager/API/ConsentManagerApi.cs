using ConsentManager.API.Entities;
using ConsentManager.API.Exceptions;
using Exiled.API.Features;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsentManager.API
{
    public static class ConsentManagerApi
    {
        private static LiteDatabase _database { get { return ConsentManager.Instance._database; } }

        /// <summary>
        /// Checks whether a player has given their consent to the server.
        /// To use, you need to register your plugin with <see cref="PluginRegistration.Register(PluginUsage)"/>
        /// and use the supplied <see cref="Guid"/> as a parameter.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="guid"></param>
        /// <returns><see cref="bool"/></returns>
        public static bool HasPlayerGivenConsent(Player player, Guid guid)
        {
            if (!PluginRegistration.IsRegistered(guid))
                throw new PluginNotRegisteredException("The supplied GUID does not match up with any other registered plugins.");

            return ConsentManager.Instance._consented.Contains(player.Id);
        }

        internal static void AddConsent(Player player)
        {
            AddPlayerToDatabase(player);
            ConsentManager.Instance._consented.Add(player.Id);
        }

        internal static void RemoveConsent(Player player)
        {
            RemovePlayerFromDatabase(player);
            ConsentManager.Instance._consented.Remove(player.Id);
        }

        internal static void RefreshConsentedPlayers()
        {
            List<PlayerConsent> consented = _database.GetCollection<PlayerConsent>().FindAll().ToList();
            ConsentManager.Instance._consented.Clear();

            foreach (PlayerConsent consent in consented)
            {
                Player player = Player.Get(consent.UserId);

                if (player != null)
                    ConsentManager.Instance._consented.Add(player.Id);
            }
        }

        internal static bool IsPlayerInDatabase(Player player)
        {
            return _database.GetCollection<PlayerConsent>().FindById(player.UserId) != null;
        }

        private static void AddPlayerToDatabase(Player player)
        {
            _database.GetCollection<PlayerConsent>().Insert(new PlayerConsent(player.UserId));
        }

        private static void RemovePlayerFromDatabase(Player player)
        {
            _database.GetCollection<PlayerConsent>().Delete(player.UserId);
        }
    }
}
