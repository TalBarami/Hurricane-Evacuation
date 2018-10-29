using System.Collections.Generic;

namespace HurricaneEvacuation.SimulatorEnvironment
{
    internal interface ISettings
    {
        IGraph Graph { get; }
        IList<IAgent> Agents { get; }
        int Deadline { get; }
        double SlowDown { get; }
        double WeightConstant { get; }
    }
}
