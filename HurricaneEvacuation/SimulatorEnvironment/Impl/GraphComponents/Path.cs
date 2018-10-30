using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents
{
    internal class Path : IPath
    {
        public IVertex Source { get; }
        public IPath Next { get; }
        public int Weight { get; }

        public Path(IVertex source, IPath next, int weight)
        {
            Source = source;
            Next = next;
            Weight = weight;
        }

        public IList<IVertex> GetVertices()
        {
            var vertices = new List<IVertex> {Source};
            var iterator = Next;

            while (iterator != null)
            {
                vertices.Add(iterator.Source);
                iterator = iterator.Next;
            }

            return vertices;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Weight: {Weight}, Path: {Source}");

            var iterator = Next;
            while (iterator != null)
            {
                sb.Append("->").Append(iterator.Source);
                iterator = iterator.Next;
            }

            return sb.ToString();
        }

        public int CompareTo(object obj)
        {
            if (obj != null && obj is IPath path)
            {
                return Weight.CompareTo(path.Weight);
            }
            throw new NotSupportedException();
        }
    }
}
