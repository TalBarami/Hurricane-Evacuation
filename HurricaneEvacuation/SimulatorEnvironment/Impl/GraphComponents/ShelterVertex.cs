namespace HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents
{
    class ShelterVertex : AbstractVertex
    {
        public ShelterVertex(int id) : base(id)
        {
        }

        public override void Accept(IAgent agent)
        {
            agent.Visit(this);
        }
    }
}
