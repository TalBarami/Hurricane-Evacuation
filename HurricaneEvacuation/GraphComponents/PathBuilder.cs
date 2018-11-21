using System.Collections.Generic;
using System.Text;
using HurricaneEvacuation.GraphComponents.Vertices;

namespace HurricaneEvacuation.GraphComponents
{
    public class PathBuilder
    {
        public IVertex Source { get; }
        public PathBuilder Next { get; }
        public int Weight { get; }

        public PathBuilder(IVertex source, PathBuilder next, int weight)
        {
            Source = source;
            Next = next;
            Weight = weight;
        }

        public Path Build(IGraph graph)
        {
            var vertices = new List<IVertex>(){ Source };
            var iterator = Next;
            while (iterator != null)
            {
                vertices.Add(iterator.Source);
                iterator = iterator.Next;
            }

            return new Path(graph, vertices);
        }

        public PathBuilder Reverse()
        {
            var currentNode = new PathBuilder(Source, null, 0);
            var currentWeight = Weight;
            var iterator = Next;

            while (iterator != null)
            {
                currentNode = new PathBuilder(iterator.Source, currentNode, currentWeight - iterator.Weight);
                iterator = iterator.Next;
            }

            return currentNode;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Weight: {Weight}, PathBuilder: {Source}");

            var iterator = Next;
            while (iterator != null)
            {
                sb.Append("->").Append(iterator.Source);
                iterator = iterator.Next;
            }

            return sb.ToString();
        }
    }
}
