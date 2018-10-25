using System.Collections.Generic;
using System.Linq;
using System.Text;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents
{
    internal class Graph : IGraph
    {
        public IList<IVertex> Vertices { get; }
        public IList<IEdge> Edges { get; }

        public Graph(IList<IVertex> vertices, IList<IEdge> edges)
        {
            Vertices = vertices;
            Edges = edges;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("\tVertices: ").Append(Vertices.ListToString())
                .Append("\n")
                .Append("\tEdges: ").Append(Edges.ListToString());
            return sb.ToString();
        }
    }
}
