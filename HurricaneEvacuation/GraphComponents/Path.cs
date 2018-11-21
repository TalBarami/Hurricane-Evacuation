using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Agents.Basic_Agents;
using HurricaneEvacuation.Environment;
using HurricaneEvacuation.GraphComponents.Vertices;

namespace HurricaneEvacuation.GraphComponents
{
    public class Path
    {
        public List<IVertex> Vertices { get; }
        public List<Edge> Edges
        {
            get
            {
                var result = new List<Edge>();
                for (var i = 0; i < Vertices.Count - 1; i++)
                {
                    result.Add(new Edge(Vertices[i], Vertices[i + 1], graph.EdgeWeight(Vertices[i], Vertices[i + 1])));
                }

                return result;
            }
        }
        public double Weight
        {
            get
            {
                var sum = 0;
                for (var i = 0; i < Vertices.Count - 1; i++)
                {
                    sum += graph.EdgeWeight(Vertices[i], Vertices[i + 1]);
                }

                return sum;
            }
        }

        public IVertex First => Vertices[0];
        public IVertex Last => Vertices[Vertices.Count - 1];
        public int Count => Vertices.Count;

        private readonly IGraph graph;

        public Path(IGraph graph, List<IVertex> vertices)
        {
            Vertices = vertices;
            this.graph = graph;
        }

        public Path(IGraph graph, List<Edge> edges)
        {
            Vertices = new List<IVertex>();
            edges.ForEach(e => Vertices.Add(e.V1));
            Vertices.Add(edges.Last().V2);
            this.graph = graph;
        }

        public Path Reverse()
        {
            var vertices = new List<IVertex>(Vertices);
            vertices.Reverse();
            return new Path(graph, vertices);
        }

        public Path Append(Path other)
        {
            var vertices = new List<IVertex>(Vertices);
            if (Last.Id == other.First.Id)
            {
                vertices.Remove(vertices.Last());
            }
            vertices.AddRange(other.Vertices);

            return new Path(graph, vertices);
        }

        public IState SimulateTraverse(IState initialState, int performer)
        {
            if (Vertices.Count <= 1)
            {
                return initialState;
            }

            var state = initialState.Clone();
            var vertex = 1;

            while (vertex < Vertices.Count)
            {
                if (state.Graph.EdgeWeight(state.Agents[performer].Position, Vertices[vertex]) <= 0)
                {
                    return initialState;
                }

                while (state.CurrentAgent != performer)
                {
                    var agent = state.Agents[state.CurrentAgent];
                    var action = agent is VandalAgent ? agent.NextStep(state) : new NoOp(state, state.Clone(), agent.Id);
                    state = action.NewState;
                }
                var traverse = new Traverse(state, state.Clone(), performer, Vertices[vertex].Id);
                state = traverse.NewState;
                vertex++;
            }

            return state;
        }

        public static Path ShortestTraversePath(List<Path> paths, IState state, int performer)
        {
            return paths.Aggregate(paths[0],
                (min, current) => current.SimulateTraverse(state.Clone(), performer).Time - state.Time <
                                  min.SimulateTraverse(state.Clone(), performer).Time - state.Time
                    ? current
                    : min);
        }

        public override string ToString()
        {
            return string.Join(" -> ", Vertices);
        }
    }
}
