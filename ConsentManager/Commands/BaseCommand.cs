using CommandSystem;
using Exiled.API.Features;
using System;

namespace ConsentManager.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class BaseCommand : ParentCommand
    {
        public override string Command { get; } = "consent";
        public override string[] Aliases { get; } = Array.Empty<string>();
        public override string Description { get; } = $"The subcommand for the {nameof(ConsentManager)} plugin.";

        public BaseCommand()
        {
            LoadGeneratedCommands();
        }

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new ConsentTerms());
            RegisterCommand(new GiveConsent());
            RegisterCommand(new RemoveConsent());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "These are the available subcommands:<color=yellow>";

            foreach (ICommand command in AllCommands)
                response += $"\n{command.Command} | {command.Description}";

            response += "</color>";
            return false;
        }
    }
}
