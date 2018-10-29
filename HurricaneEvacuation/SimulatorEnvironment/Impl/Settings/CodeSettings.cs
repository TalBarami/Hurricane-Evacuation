using System.Collections.Generic;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Settings
{
    internal class CodeSettings : AbstractSettings
    {
        public CodeSettings(IGraph world, IList<IAgent> agents, int deadline, double slowDown, double weightConstant)
        {
            Graph = world;
            Agents = agents;
            Deadline = deadline;
            SlowDown = slowDown;
            WeightConstant = weightConstant;
        }
    }
}
