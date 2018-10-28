using System.Collections.Generic;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Settings
{
    internal class CodeSettings : AbstractSettings
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
