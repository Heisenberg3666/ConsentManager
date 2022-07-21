using CommandSystem;
using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;

namespace ConsentManager.Commands
{
    internal class GiveConsent : ICommand
    {
        private List<int> _confirmed { get; } = new List<int>();
        private List<int> _toConfirm { get; } = new List<int>();

        public string Command { get; } = nameof(GiveConsent).ToLower();
        public string[] Aliases { get; } = new string[] { "give", "add" };
        public string Description { get; } = "Give explicit consent to let the server know that you are okay with them storing your information.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            bool consentGiven = ConsentManager.Instance._api.HasPlayerGivenConsent(player);

            if (!_toConfirm.Contains(player.Id)
                && !_confirmed.Contains(player.Id)
                && !consentGiven)
            {
                response = "Run the same command within the next 10 seconds to confirm." +
                    "\nBy confirming, you accept the terms listed in <color=red>.consent terms</color>.";
                _toConfirm.Add(player.Id);

                Timing.CallDelayed(10f, () =>
                {
                    if (_confirmed.Contains(player.Id))
                        ConsentManager.Instance._api.AddConsent(player);

                    _toConfirm.Remove(player.Id);
                    _confirmed.Remove(player.Id);
                });

                return true;
            }
            else if (!_confirmed.Contains(player.Id)
                && !consentGiven)
            {
                _confirmed.Add(player.Id);
                _toConfirm.Remove(player.Id);

                response = "You have sucessfully given explicit consent to the server.";
                return true;
            }
            else
            {
                response = "You have already given consent to this server.";
                return false;
            }
        }
    }
}
