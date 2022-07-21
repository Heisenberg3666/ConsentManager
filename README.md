# ConsentManager

![GitHub release (latest by date)](https://img.shields.io/github/downloads/Heisenberg3666/ConsentManager/total?style=for-the-badge)
[![GPLv3 License](https://img.shields.io/badge/License-GPL%20v3-yellow.svg?style=for-the-badge)](https://opensource.org/licenses/)

ConsentManager is an SCP: SL plugin using the Exiled framework. The purpose of this plugin is to give players the option to give or remove their consent for the server to store their information.

## Authors

- [@Heisenberg3666](https://github.com/Heisenberg3666)

## Installation

Download ConsentManager.dll from the latest release and place inside of the Plugins folder.
You will also need to download LiteDB.dll aswell so that the plugin works.

## Support

For support, please create an issue on GitHub or message me on Discord (Heisenberg#3666).

## Features

- Allows players to consent to servers storing their information.
- Can be used by other plugins to tell if a player has consented.

## Developers

### API Examples

```csharp
using ConsentManager.API;
using ConsentManager.API.Entities;
...
PluginUsage pluginUsage = new PluginUsage()
{
    Name = "MyPlugin",
    Version = new Version(1, 0, 0, 0),
    DataUsage = "MyPlugin will use player's data to...",
    WhoCanSeeData = "Only MyPlugin can use/see the player's data."
};

Guid apiKey = PluginRegistration.Register(pluginUsage);
...
Player player = Player.Get("Heisenberg");

if (ConsentManagerApi.HasPlayerGivenConsent(player, apiKey))
{
    ...
}
```

## License

[GPLv3](https://choosealicense.com/licenses/gpl-3.0/)
