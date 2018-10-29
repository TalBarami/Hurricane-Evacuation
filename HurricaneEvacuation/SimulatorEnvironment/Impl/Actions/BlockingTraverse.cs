namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Actions
{
    internal class BlockingTraverse : Traverse
    {
        protected IEdge BlockEdge;
        public BlockingTraverse(IAgent agent, IEdge destination, IEdge blockEdge) : base(agent, destination)
        {
            BlockEdge = blockEdge;
            Cost = 1 + base.Cost;
        }

        public override double Cost { get; }

        public override void Approve()
        {
            base.Approve();
            BlockEdge.Blocked = true;
        }

        public override string ToString()
        {
            return $"{base.ToString()} and block the path to {BlockEdge}";
        }
    }
}
