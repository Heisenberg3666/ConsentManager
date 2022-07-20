using Exiled.API.Features;
using Exiled.API.Interfaces;
using System.ComponentModel;
using System.IO;

namespace ConsentManager.Configs
{
    public class BaseConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool DebugMode { get; set; } = true;

        [Description("This is the path to where the _database is stored.")]
        public string DatabasePath { get; set; } = Path.Combine(Paths.Configs, "Consent.db");
    }
}
