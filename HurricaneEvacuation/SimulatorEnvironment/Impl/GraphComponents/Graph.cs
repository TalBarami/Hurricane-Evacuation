using System.Collections.Generic;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents
{
    class Graph : IGraph
    {
        public int Deadline { get; set; }
        public IList<IVertex> Vertices { get; }

        public Graph(int deadline, IList<IVertex> vertices)
        {
            Deadline = deadline;
            Vertices = vertices;
        }
    }
}
