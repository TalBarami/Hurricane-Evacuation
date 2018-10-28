using System;

namespace HurricaneEvacuation.SimulatorEnvironment.Exceptions
{
    internal class ParseException : Exception
    {

        public ParseException()
        {
        }

        public ParseException(string message) : base(message)
        {
        }

        public ParseException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
