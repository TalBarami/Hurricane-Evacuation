using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    internal class GreedyAgent : AbstractAgent
    {
        public GreedyAgent(int id, IVertex position) : base(position)
        {
            Id = $"GreedyAgent{id}";
        }

        public override IAction PlayNext(IGraph world)
        {
            // If carry - find shelter. Else, or if can't reach shelter - find people, Else or if can't reach people, nop.
            return null;
        }
    }
}
