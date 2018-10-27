using System;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents
{
    internal class Edge : IEdge
    {
        public IVertex V1 { get; }
        public IVertex V2 { get; }
        public int Weight { get; }
        public bool Blocked { get; set; }

        public Edge (IVertex v1, IVertex v2, int weight)
        {
            V1 = v1;
            V2 = v2;
            Weight = weight;
        }

        public bool Contains(IVertex v1)
        {
            return V1.Equals(v1) || V2.Equals(v1);
        }

        public bool Contains(IVertex v1, IVertex v2)
        {
            return Contains(v1) && Contains(v2);
        }

        public IVertex OtherV(IVertex v)
        {
            return v.Equals(V1) ? V2 : V1;
        }

        public override string ToString()
        {
            var blockedStatus = Blocked ? "B" : "";
            return $"({V1.Id},{V2.Id})W{Weight}{blockedStatus}";
        }

        public int CompareTo(object obj)
        {
            if (obj is IEdge edge)
            {
                return Weight.CompareTo(edge.Weight);
            }

            throw new Exception("Invalid object type");
        }
    }
}
