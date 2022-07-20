using CommandSystem;
using ConsentManager.API;
using ConsentManager.Configs;
using Exiled.API.Features;
using System;

namespace ConsentManager.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    internal class Consent : ICommand, IUsageProvider
    {
        public string Command { get; } = nameof(Command).ToLower();
        public string[] Aliases { get; } = new string[] { "giveconsent" };
        public string Description { get; } = "Give consent for this server to store information about you to help make gameplay more fun.";
        public string[] Usage { get; } = new string[] { "Consent [true/false]" };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            BaseConfig config = ConsentManager.Instance.Config;
            Translation translation = ConsentManager.Instance.Translation;

            Player player = Player.Get(sender);

            if (arguments.Count < 1)
            {
                response = translation.InvalidArgumentCount;
                return false;
            }

            if (!bool.TryParse(arguments.At(0), out bool consent))
            {
                response = translation.InvalidArgumentType;
                return false;
            }

            if (player.GivenConsent())
            {
                if (consent)
                {
                    response = translation.AlreadyConsented;
                    return false;
                }
                else
                {
                    player.RemoveConsent();
                    Log.Debug($"{player.Nickname} has removed consent.", config.DebugMode);

                    response = translation.RemovedConsent;
                    return true;
                }
            }
            else
            {
                if (consent)
                {
                    player.AddConsent();
                    Log.Debug($"{player.Nickname} has given consent.", config.DebugMode);

                    response = translation.AddedConsent;
                    return true;
                }
                else
                {
                    response = translation.AlreadyRemovedConsent;
                    return false;
                }
            }
        }
    }
}
