using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment
{
    internal interface IGraph
    {
        IList<IVertex> Vertices { get; }
        IList<IEdge> Edges { get; }
    }
}
