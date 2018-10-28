using System.Collections.Generic;

namespace HurricaneEvacuation.SimulatorEnvironment
{
    internal interface IGraph
    {
        IList<IVertex> Vertices { get; }
        IList<IEdge> Edges { get; }
    }
}
