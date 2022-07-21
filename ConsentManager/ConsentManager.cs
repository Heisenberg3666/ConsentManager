using ConsentManager.API;
using ConsentManager.API.Entities;
using ConsentManager.Configs;
using ConsentManager.Events;
using Exiled.API.Features;
using LiteDB;
using System;
using System.Collections.Generic;

namespace ConsentManager
{
    public class ConsentManager : Plugin<BaseConfig, Translation>
    {
        private PlayerEvents _playerEvents;

        internal List<int> _consented;
        internal ConsentManagerApi _api;
        internal LiteDatabase _database;

        public static ConsentManager Instance;

        public override string Name { get; } = "ConsentManager";
        public override string Author { get; } = "Heisenberg3666";
        public override Version Version { get; } = new Version(1, 0, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(5, 2, 2);

        public override void OnEnabled()
        {
            Instance = this;

            _consented = new List<int>();
            _database = new LiteDatabase(Config.DatbaseFile);

            _api = PluginRegistration.Register(new PluginUsage()
            {
                Name = nameof(ConsentManager),
                Version = Version,
                DataUsage = "Player's data will be used for deciding if they have DNT (Do Not Track) enabled.",
                WhoCanSeeData = $"Any other plugins that use the {nameof(ConsentManager)} plugin (this plugin)."
            });

            _api.RefreshConsentedPlayers();

            _playerEvents = new PlayerEvents(Translation, _api);

            RegisterEvents();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();

            _playerEvents = null;

            _database.Dispose();
            _database = null;
            _consented = null;

            PluginRegistration.Unregister(_api.Guid);
            _api = null;

            Instance = null;

            base.OnDisabled();
        }
        
        private void RegisterEvents()
        {
            _playerEvents.RegisterEvents();
        }

        private void UnregisterEvents()
        {
            _playerEvents.UnregisterEvents();
        }
    }
}
