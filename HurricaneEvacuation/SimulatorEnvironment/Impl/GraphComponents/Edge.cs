namespace HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents
{
    class Edge : IEdge
    {
        public IVertex V1 { get; }
        public IVertex V2 { get; }
        public bool Blocked { get; set; }

        public Edge (IVertex v1, IVertex v2)
        {
            V1 = v1;
            V2 = v2;
        }

        public bool ConnectedTo(IVertex v)
        {
            return V1.Equals(v) || V2.Equals(v);
        }
    }
}
