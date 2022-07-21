using ConsentManager.API.Entities;
using ConsentManager.API.Exceptions;
using System;
using System.Collections.Generic;

namespace ConsentManager.API
{
    public static class PluginRegistration
    {
        internal static Dictionary<Guid, PluginUsage> _plugins = new Dictionary<Guid, PluginUsage>();

        public static Guid Register(PluginUsage pluginUsage)
        {
            Guid guid = Guid.NewGuid();
            _plugins.Add(guid, pluginUsage);

            return guid;
        }

        public static void Unregister(Guid guid)
        {
            if (!IsRegistered(guid))
                throw new PluginNotRegisteredException("The supplied GUID does not match up with any other registered plugins.");

            _plugins.Remove(guid);
        }

        internal static bool IsRegistered(Guid guid)
        {
            return _plugins.ContainsKey(guid);
        }
    }
}
