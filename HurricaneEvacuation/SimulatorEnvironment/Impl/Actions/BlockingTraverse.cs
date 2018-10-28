using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Settings;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Actions
{
    class BlockingTraverse : Traverse
    {
        public BlockingTraverse(IAgent agent, IEdge edge) : base(agent, edge)
        {
            edge.Blocked = true;
            Cost = 1 + base.Cost;
        }

        public new double Cost { get; }

        public override string ToString()
        {
            return $"{base.ToString()} and block the path";
        }
    }
}
