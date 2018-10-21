using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment
{
    interface IGraph
    {
        int Deadline { get; set; }
        IList<IVertex> Vertices { get; }

    }
}
