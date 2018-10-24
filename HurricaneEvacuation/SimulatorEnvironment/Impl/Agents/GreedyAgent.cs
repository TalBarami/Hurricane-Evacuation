using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    class GreedyAgent : AbstractAgent
    {
        public GreedyAgent(IVertex position) : base(position)
        {
        }

        public override IAction PerformStep()
        {
            if (Passengers > 0)
            {
                // Search for shelter
            }
            else
            {
                // Search for people
            }
            return new NoOperation();
        }
    }
}
