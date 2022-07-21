using CommandSystem;
using Exiled.API.Features;
using System;

namespace ConsentManager.Commands
{
    internal class RemoveConsent : ICommand
    {
        public string Command { get; } = nameof(RemoveConsent).ToLower();
        public string[] Aliases { get; } = new string[] { "remove", "revoke" };
        public string Description { get; } = "Removes your consent from the server.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (ConsentManager.Instance._api.HasPlayerGivenConsent(player))
            {
                ConsentManager.Instance._api.RemoveConsent(player);

                response = "You have sucessfully removed your consent." +
                    "\nIf you do not have DNT (Do Not Track) enabled, you will need to " +
                    "run this command again next round if you still do not consent.";
                return true;
            }
            else
            {
                response = "You have not given consent, you cannot remove what doesn't exist.";
                return false;
            }
        }
    }
}
