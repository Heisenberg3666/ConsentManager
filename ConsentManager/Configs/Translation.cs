using Exiled.API.Interfaces;

namespace ConsentManager.Configs
{
    public class Translation : ITranslation
    {
        public string PopupMessage { get; set; } = "You can consent to this server storing information by using the .consent command.";

        public string InvalidArgumentCount { get; set; } = "You have not supplied enough arguments.";
        public string InvalidArgumentType { get; set; } = "The argument needs to be either true or false.";
        public string AlreadyConsented { get; set; } = "You have already given consent.";
        public string AlreadyRemovedConsent { get; set; } = "You have already removed consent.";
        public string AddedConsent { get; set; } = "You have sucessfully given consent.";
        public string RemovedConsent { get; set; } = "You have sucessfully removed consent.";
    }
}
