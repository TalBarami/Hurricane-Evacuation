using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    abstract class AbstractAiAgent : AbstractAgent
    {
        protected IHeuristicFunction HeuristicFunction;

        protected AbstractAiAgent(int id, IVertex position, IHeuristicFunction heuristicFunction) : base(id, position)
        {
            HeuristicFunction = heuristicFunction;
        }
    }
}
