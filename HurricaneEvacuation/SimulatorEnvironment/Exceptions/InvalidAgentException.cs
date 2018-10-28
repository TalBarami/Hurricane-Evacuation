using System;

namespace HurricaneEvacuation.SimulatorEnvironment.Exceptions
{
    internal class InvalidAgentIdException : Exception
    {
        public InvalidAgentIdException()
        {
        }

        public InvalidAgentIdException(string message) : base(message)
        {
        }

        public InvalidAgentIdException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
