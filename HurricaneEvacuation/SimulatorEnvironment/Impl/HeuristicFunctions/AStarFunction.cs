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

        public override double Value(ISettings settings, IVertex source, double time)
        {
            return PathSoFar.Value(settings, source, time) +
                   UnreachablePeople.Value(settings, source, time);
        }
    }
}
