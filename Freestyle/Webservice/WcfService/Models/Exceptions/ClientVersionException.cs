using System;

namespace WcfService.Models.Exceptions
{
    [Serializable]
    public class ClientVersionException : Exception
    {
        public ClientVersionException() { }

        public ClientVersionException(string message) : base(message) { }

        public ClientVersionException(string message, Exception inner) : base(message, inner) { }
    }
}