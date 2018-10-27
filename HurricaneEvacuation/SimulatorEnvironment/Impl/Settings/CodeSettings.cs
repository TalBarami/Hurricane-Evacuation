using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Settings
{
    class CodeSettings : AbstractSettings
    {
        public CodeSettings(IGraph world, IList<IAgent> agents, int deadline, double slowDown)
        {
            Graph = world;
            Agents = agents;
            Deadline = deadline;
            SlowDown = slowDown;
        }
    }
}
