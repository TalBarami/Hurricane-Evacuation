using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment
{
    interface ISettings
    {
        IGraph Graph { get; }
        IList<IAgent> Agents { get; }
        int Deadline { get; }
        double SlowDown { get; }
    }
}
