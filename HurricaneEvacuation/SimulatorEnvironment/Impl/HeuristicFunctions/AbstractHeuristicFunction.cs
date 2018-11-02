using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions
{
    abstract class AbstractHeuristicFunction : IHeuristicFunction
    {

        protected AbstractHeuristicFunction()
        {
        }

        public abstract HeuristicResult Apply(IAgent agent, Traverse move, double time);
    }
}
