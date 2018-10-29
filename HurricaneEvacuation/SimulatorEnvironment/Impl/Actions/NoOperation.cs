namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Actions
{
    internal class NoOperation : IAction
    {
        public IVertex Destination { get; set; }

        public NoOperation(IVertex destination)
        {
            Destination = destination;
            Cost = 1;
        }

        public double Cost { get; }
        public void Approve()
        {
        }

        public override string ToString()
        {
            return "do nothing";
        }
    }
}
