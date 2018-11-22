using System.Collections.Generic;
using System.Linq;
using System.Text;
using HurricaneEvacuation.GraphComponents.Vertices;

namespace HurricaneEvacuation.GraphComponents
{
    internal class Graph : IGraph
    {
        public List<IVertex> Vertices { get; }

        public List<Edge> Edges
        {
            get
            {
                var edges = new List<Edge>();
                for (var i = 0; i < Vertices.Count; i++)
                {
                    for (var j = i; j < Vertices.Count; j++)
                    {
                        var w = adjacencyMatrix[i, j];
                        if (w > 0)
                        {
                            edges.Add(new Edge(Vertex(i), Vertex(j), w));
                        }
                    }
                }

                return edges;
            }
        }
        private readonly int[,] adjacencyMatrix;

        public Graph(List<IVertex> vertices)
        {
            Vertices = vertices.Select(v => v.Clone()).ToList();
            Vertices.ForEach(v => v.Graph = this);
            adjacencyMatrix = new int[vertices.Count, vertices.Count];
        }

        public Graph(Graph other) : this(other.Vertices)
        {
            adjacencyMatrix = other.adjacencyMatrix.Clone() as int[,];
        }

        public IVertex Vertex(int id)
        {
            return Vertices.First(v => v.Id == id);
        }

        public void AddEdge(int v1, int v2, int weight)
        {
            adjacencyMatrix[v1, v2] = weight;
            adjacencyMatrix[v2, v1] = weight;
            var vertex1 = Vertex(v1);
            var vertex2 = Vertex(v2);
            vertex1.Neighbors.Add(v2);
            vertex2.Neighbors.Add(v1);
        }

        public List<Path> Dfs(int src, int dst)
        {
            return Dfs(Vertex(src), Vertex(dst));
        }

        public List<Path> Dfs(IVertex src, IVertex dst)
        {
            if (src.Equals(dst))
            {
                return new List<Path> {new Path(this, new List<IVertex> {src})};
            }
            var lst = new List<Path>();
            var queue = new Queue<QueueItem>();
            queue.Enqueue(new QueueItem(src, new List<Edge>()));
            while (queue.Count > 0)
            {
                var currentItem = queue.Dequeue();
                foreach (var edge in currentItem.Node.Edges)
                {
                    if (!currentItem.Visited.Contains(edge))
                    {
                        List<Edge> visited = new List<Edge>(currentItem.Visited);
                        visited.Add(edge);
                        if (edge.V2 == dst)
                        {
                            lst.Add(new Path(this, visited));
                        }
                        else
                        {
                            queue.Enqueue(new QueueItem(edge.V2, visited));
                        }
                    }
                }
            }

            return lst;
        }

        internal class QueueItem
        {

            public IVertex Node { get; private set; }
            public List<Edge> Visited { get; private set; }

            public QueueItem(IVertex node, List<Edge> visited)
            {
                Node = node;
                Visited = visited;
            }

        }

        public List<Path> Dijkstra(int source)
        {
            return Dijkstra(Vertex(source));
        }

        public List<Path> Dijkstra(IVertex source)
        {
            var paths = new Dictionary<IVertex, PathBuilder>();
            var flags = new Dictionary<IVertex, bool>();

            var vertices = new List<IVertex>(Vertices);
            foreach (var v in vertices)
            {
                paths[v] = new PathBuilder(v, null, v == source ? 0 : int.MaxValue);
                flags[v] = false;
            }

            var unreachable = new Vertex(-1);
            while (true)
            {
                var least = new PathBuilder(unreachable, null, int.MaxValue);
                foreach (var v in vertices)
                {
                    if (!flags[v] && paths[v].Weight < least.Weight)
                    {
                        least = paths[v];
                    }
                }

                if (least.Source == unreachable)
                {
                    break;
                }

                var src = least.Source;
                flags[src] = true;

                foreach (var dst in src.Neighbors.Select(Vertex))
                {
                    var weight = EdgeWeight(src, dst);
                    if (weight <= 0 || flags[dst]) continue;

                    var oldPath = paths[dst];
                    var newWeight = weight + least.Weight;
                    if (newWeight < oldPath.Weight)
                    {
                        paths[oldPath.Source] = new PathBuilder(oldPath.Source, least, newWeight);
                    }
                }
            }

            var pathsFound = new List<PathBuilder>(paths.Values);
            var result = pathsFound.Select(p => p.Build(this).Reverse()).OrderBy(p => p.First.Id).ToList();
            return result;
        }

        public IGraph Clone()
        {
            return new Graph(this);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("\tVertices: ").Append(string.Join(", ", Vertices)).Append("\n")
                .Append("\tEdges: ").Append(string.Join(", ", Edges));
            return sb.ToString();
        }

        public int EdgeWeight(int v1, int v2)
        {
            var w = adjacencyMatrix[v1, v2];
            return w <= 0 ? int.MaxValue : w;
        }
        public int EdgeWeight(int v1, IVertex v2)
        {
            return EdgeWeight(v1, v2.Id);
        }
        public int EdgeWeight(IVertex v1, int v2)
        {
            return EdgeWeight(v1.Id, v2);
        }
        public int EdgeWeight(IVertex v1, IVertex v2)
        {
            return EdgeWeight(v1.Id, v2.Id);
        }

        public void RemoveEdge(int v1, int v2)
        {
            RemoveEdge(Vertex(v1), Vertex(v2));
        }

        public void RemoveEdge(int v1, IVertex v2)
        {
            RemoveEdge(Vertex(v1), v2);
        }

        public void RemoveEdge(IVertex v1, int v2)
        {
            RemoveEdge(v1, Vertex(v2));
        }

        public void RemoveEdge(IVertex v1, IVertex v2)
        {
            adjacencyMatrix[v1.Id, v2.Id] = 0;
            adjacencyMatrix[v2.Id, v1.Id] = 0;
            v1.Neighbors.Remove(v2.Id);
            v2.Neighbors.Remove(v1.Id);
        }

    }
}
