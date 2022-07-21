using ConsentManager.API.Entities;
using ConsentManager.API.Exceptions;
using Exiled.API.Features;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsentManager.API
{
    public class ConsentManagerApi
    {
        private LiteDatabase _database;

        private bool IsRegistered => PluginRegistration.IsRegistered(Guid);

        public Guid Guid;

        public ConsentManagerApi(Guid guid)
        {
            _database = ConsentManager.Instance._database;

            Guid = guid;

            if (!IsRegistered)
                throw new PluginNotRegisteredException("The supplied GUID does not match up with any other registered plugins.");
        }

        public bool HasPlayerGivenConsent(Player player)
        {
            return ConsentManager.Instance._consented.Contains(player.Id);
        }

        internal void AddConsent(Player player)
        {
            AddPlayerToDatabase(player);
            ConsentManager.Instance._consented.Add(player.Id);
        }

        internal void RemoveConsent(Player player)
        {
            RemovePlayerFromDatabase(player);
            ConsentManager.Instance._consented.Remove(player.Id);
        }

        internal void RefreshConsentedPlayers()
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

        internal bool IsPlayerInDatabase(Player player)
        {
            return _database.GetCollection<PlayerConsent>().FindById(player.UserId) != null;
        }

        private void AddPlayerToDatabase(Player player)
        {
            _database.GetCollection<PlayerConsent>().Insert(new PlayerConsent(player.UserId));
        }

        private void RemovePlayerFromDatabase(Player player)
        {
            _database.GetCollection<PlayerConsent>().Delete(player.UserId);
        }
    }
}
