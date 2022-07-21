using CommandSystem;
using ConsentManager.API;
using ConsentManager.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsentManager.Commands
{
    internal class ConsentTerms : ICommand
    {
        public string Command { get; } = nameof(ConsentTerms).ToLower();
        public string[] Aliases { get; } = new string[] { "terms", "conditions" };
        public string Description { get; } = "Lists all of the terms that you will accept before giving explicit consent.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "Run <color=red>.consent give</color> to agree to these terms and conditions." + 
                "\nThese plugins will use and store your data, every plugin defines what is is used for and who can see it below:\n";

            IEnumerable<ConsentManagerApi> pluginUsages = PluginRegistration._apis.Values.ToList();

            for (int i = 0; i < PluginRegistration._apis.Count; i++)
            {
                PluginUsage pluginUsage = pluginUsages.ToList()[i]._pluginUsage;
                int pluginNumber = i + 1;

                response += $"\n{pluginNumber}.1 - Plugin name: <color=green>{pluginUsage.Name}</color>" +
                            $"\n{pluginNumber}.2 - Plugin version: <color=green>{pluginUsage.Version}</color>" +
                            $"\n{pluginNumber}.3 - Who can see my data: <color=green>{pluginUsage.WhoCanSeeData}</color>" +
                            $"\n{pluginNumber}.4 - What is your data used for: <color=green>{pluginUsage.DataUsage}</color>\n";
            }

            response += "\nYou can remove your consent at any time by running <color=red>.consent remove</color>.";
            return true;
        }
    }
}
