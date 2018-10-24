using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Actions
{
    class Traverse : IAction
    {
        public int Cost(IAgent a, IEdge e)
        {
            return e.Weight * (1 + a.Passengers);
        }
    }
}
