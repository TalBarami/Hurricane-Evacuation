namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Actions
{
    internal class BlockingTraverse : Traverse
    {
        public BlockingTraverse(IAgent agent, IEdge edge) : base(agent, edge)
        {
            edge.Blocked = true;
            Cost = 1 + base.Cost;
        }

        public new double Cost { get; }

        public override string ToString()
        {
            return $"{base.ToString()} and block the path";
        }
    }
}
