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
    }
}
