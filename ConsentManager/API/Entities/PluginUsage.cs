using System;

namespace ConsentManager.API.Entities
{
    public class PluginUsage
    {
        /// <summary>
        /// What is the name of the plugin?
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// What is the plugin's version?
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// What will the player's data be used for?
        /// </summary>
        public string DataUsage { get; set; }

        /// <summary>
        /// Who can see the player's data?
        /// </summary>
        public string WhoCanSeeData { get; set; }
    }
}
