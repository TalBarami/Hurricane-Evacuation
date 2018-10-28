namespace HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents
{
    internal class ShelterVertex : AbstractVertex
    {
        public ShelterVertex(int id) : base(id)
        {
        }

        public override void Accept(IAgent agent)
        {
            agent.Visit(this);
        }
        public override void Accept(IHeuristicFunction heuristicFunction)
        {
            heuristicFunction.Visit(this);
        }

        public override string ToString()
        {
            return $"{base.ToString()}S";
        }
    }
}
