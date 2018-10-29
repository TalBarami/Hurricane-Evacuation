using System.Collections.Generic;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Settings
{
    internal class AbstractSettings : ISettings
    {
        public IGraph Graph { get; protected set; }
        public IList<IAgent> Agents { get; protected set; }
        public int Deadline { get; protected set; }
        public double SlowDown { get; protected set; }
        public double WeightConstant { get; protected set; }

        public override string ToString()
        {
            return $"Deadline: {Deadline}\nGraph state:\n{Graph}";
        }
    }
}
