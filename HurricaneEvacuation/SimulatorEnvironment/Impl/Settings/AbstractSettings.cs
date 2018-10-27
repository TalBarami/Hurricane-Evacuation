using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Settings
{
    class AbstractSettings : ISettings
    {
        public IGraph Graph { get; protected set; }
        public IList<IAgent> Agents { get; protected set; }
        public int Deadline { get; protected set; }
        public double SlowDown { get; protected set; }

        public override string ToString()
        {
            return $"Deadline: {Deadline}\nGraph state:\n{Graph}";
        }
    }
}
