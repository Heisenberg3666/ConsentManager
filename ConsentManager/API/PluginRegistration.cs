using ConsentManager.API.Entities;
using ConsentManager.API.Exceptions;
using System;
using System.Collections.Generic;

namespace ConsentManager.API
{
    public static class PluginRegistration
    {
        private static Dictionary<Guid, ConsentManagerApi> _apis = new Dictionary<Guid, ConsentManagerApi>();

        internal static Dictionary<Guid, PluginUsage> _plugins = new Dictionary<Guid, PluginUsage>();

        public static ConsentManagerApi Register(PluginUsage pluginUsage)
        {
            Guid guid = Guid.NewGuid();
            _plugins.Add(guid, pluginUsage);
            _apis.Add(guid, null);
            _apis[guid] = new ConsentManagerApi(guid);

            return _apis[guid];
        }

        public static void Unregister(Guid guid)
        {
            if (!IsRegistered(guid))
                throw new PluginNotRegisteredException("The supplied GUID does not match up with any other registered plugins.");

            _plugins.Remove(guid);
            _apis.Remove(guid);
        }

        internal static bool IsRegistered(Guid guid)
        {
            return _plugins.ContainsKey(guid);
        }
    }
}
