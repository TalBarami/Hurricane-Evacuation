using System.Collections.Generic;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents
{
    class Graph : IGraph
    {
        public IList<IVertex> Vertices { get; }
        public IList<IEdge> Edges { get; }

        public Graph(IList<IVertex> vertices, IList<IEdge> edges)
        {
            Vertices = vertices;
            Edges = edges;
        }
    }
}
