using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Actions
{
    internal class NoOperation : IAction
    {
        public IVertex Destination { get; set; }

        public NoOperation(IVertex destination)
        {
            Destination = destination;
        }

        public double Cost => 1;

        public override string ToString()
        {
            return "do nothing";
        }
    }
}
