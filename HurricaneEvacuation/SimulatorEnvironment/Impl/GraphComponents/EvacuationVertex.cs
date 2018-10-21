namespace HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents
{
    class EvacuationVertex : AbstractVertex
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
    }
}

