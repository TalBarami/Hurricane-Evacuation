namespace HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents
{
    internal class EmptyVertex : AbstractVertex
    {
        public EmptyVertex(int id) : base(id)
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
    }
}
