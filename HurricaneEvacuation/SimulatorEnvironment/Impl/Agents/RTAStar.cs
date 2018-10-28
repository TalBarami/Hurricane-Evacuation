using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    class RTAStar : AbstractAiAgent
    {
        public RTAStar(int id, IVertex position, IHeuristicFunction heuristicFunction) : base(id, position, heuristicFunction)
        {
        }

        public override IAction PlayNext()
        {
            throw new NotImplementedException();
        }
    }
}
