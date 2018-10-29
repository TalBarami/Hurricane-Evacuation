namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Actions
{
    internal class BlockingTraverse : Traverse
    {
        public BlockingTraverse(IAgent agent, IEdge edge) : base(agent, edge)
        {
            Cost = 1 + base.Cost;
        }

        public override double Cost { get; }

        public override void Approve()
        {
            base.Approve();
            Edge.Blocked = true;
        }

        public override string ToString()
        {
            return $"{base.ToString()} and block the path";
        }
    }
}
