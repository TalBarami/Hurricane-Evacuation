using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions
{
    class AStarFunction : AbstractHeuristicFunction
    {
        private PathAccumulatorFunction PathSoFar { get; }
        private UnreachablePeopleFunction UnreachablePeople { get; }

        public override double Value(IGraph graph, IVertex source, double time, double deadline)
        {
            return PathSoFar.Value(graph, source, time, deadline) +
                   UnreachablePeople.Value(graph, source, time, deadline);
        }
    }
}
