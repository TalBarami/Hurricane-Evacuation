using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment
{
    interface IPath
    {
        IVertex Source { get; }
        IPath Next { get; }
        int Weight { get; }

    }
}
