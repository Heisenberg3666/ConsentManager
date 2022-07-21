using ConsentManager.API.Entities;
using ConsentManager.API.Exceptions;
using System;
using System.Collections.Generic;

namespace ConsentManager.API
{
    public static class PluginRegistration
    {
        internal static Dictionary<Guid, ConsentManagerApi> _apis = new Dictionary<Guid, ConsentManagerApi>();

        public static ConsentManagerApi Register(PluginUsage pluginUsage)
        {
            Guid guid = Guid.NewGuid();
            _apis.Add(guid, null);
            _apis[guid] = new ConsentManagerApi(guid, pluginUsage);

            return _apis[guid];
        }

        public static void Unregister(ConsentManagerApi api)
        {
            if (!IsRegistered(api._guid))
                throw new PluginNotRegisteredException("The supplied GUID does not match up with any other registered plugins.");

            _apis.Remove(api._guid);
        }

        internal static bool IsRegistered(Guid guid)
        {
            return _apis.ContainsKey(guid);
        }
    }
}
