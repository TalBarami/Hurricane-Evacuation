using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions
{
    abstract class AbstractHeuristicFunction : IHeuristicFunction
    {

        protected AbstractHeuristicFunction()
        {
        }
        public abstract double Value(IGraph graph, IVertex source, double time, double deadline);
    }
}
