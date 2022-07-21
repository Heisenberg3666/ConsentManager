using System;

namespace ConsentManager.API.Exceptions
{
    public class PluginNotRegisteredException : Exception
    {
        public PluginNotRegisteredException(string msg) : base(msg)
        {
        }
    }
}
