using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    class HumanAgent : AbstractAgent
    {
        public HumanAgent(IVertex position) : base(position)
        {
        }

        public override IAction PerformStep(IGraph world)
        {
            throw new NotImplementedException();
        }
    }
}
