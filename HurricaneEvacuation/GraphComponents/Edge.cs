using HurricaneEvacuation.GraphComponents.Vertices;

namespace HurricaneEvacuation.GraphComponents
{
    internal class Edge
    {
        public IVertex V1 { get; }
        public IVertex V2 { get; }
        public double Weight { get; }
        public Edge(IVertex v1, IVertex v2, double weight)
        {
            V1 = v1;
            V2 = v2;
            Weight = weight;
        }

        public override string ToString()
        {
            return $"({V1.Id},{V2.Id})W{Weight}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Edge other)
            {
                return (V1.Equals(other.V1) || V1.Equals(other.V2)) &&
                       (V2.Equals(other.V1) || V2.Equals(other.V2)) &&
                       Weight.Equals(other.Weight);
            }

            return false;
        }
    }
}
