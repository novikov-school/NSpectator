using System;

namespace NSpectator.Domain
{
    public class ExceptionNotThrown : Exception
    {
        public ExceptionNotThrown(string message)
            : base(message) {}
    }
}