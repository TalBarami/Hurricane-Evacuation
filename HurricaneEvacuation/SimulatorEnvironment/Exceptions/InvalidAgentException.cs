using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment.Exceptions
{
    class InvalidAgentIdException : Exception
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
