namespace HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents
{
    internal class EvacuationVertex : AbstractVertex
    {
        public int PeopleCount { get; set; }

        public EvacuationVertex(int id, int peopleCount) : base(id)
        {
            PeopleCount = peopleCount;
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
            return $"{base.ToString()}P{PeopleCount}";
        }
    }
}

