using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents
{
    internal class EmptyVertex : AbstractVertex
    {
        public EmptyVertex(int id) : base(id)
        {
        }

        public override void Accept(IAgent agent)
        {
            agent.Visit(this);
        }

        public override void Accept(IHeuristicFunction heuristicFunction)
        {
            heuristicFunction.Visit(this);
        }
    }
}
