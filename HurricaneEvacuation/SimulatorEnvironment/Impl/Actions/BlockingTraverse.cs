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
        }

        public new double Cost => 1 + edge.Weight * (1 + currentPassengers * SettingsSingleton.Instance.SlowDown);

        public override string ToString()
        {
            return $"{base.ToString()} and block the path";
        }
    }
}
