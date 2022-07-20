using ConsentManager.Configs;
using ConsentManager.Events;
using Exiled.API.Features;
using LiteDB;
using System;

namespace ConsentManager
{
    public class ConsentManager : Plugin<BaseConfig, Translation>
    {
        private PlayerEvents _playerEvents;

        internal LiteDatabase _database;

        public static ConsentManager Instance;

        public override string Name { get; } = "ConsentManager";
        public override string Author { get; } = "Heisenberg3666";
        public override Version Version { get; } = new Version(1, 0, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(5, 2, 2);

        public override void OnEnabled()
        {
            Instance = this;

            _database = new LiteDatabase(Config.DatabasePath);

            _playerEvents = new PlayerEvents(Translation);

            RegisterEvents();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();

            _playerEvents = null;

            _database.Dispose();
            _database = null;

            Instance = null;

            base.OnDisabled();
        }

        public void RegisterEvents()
        {
            _playerEvents.RegisterEvents();
        }

        public void UnregisterEvents()
        {
            _playerEvents.UnregisterEvents();
        }
    }
}
