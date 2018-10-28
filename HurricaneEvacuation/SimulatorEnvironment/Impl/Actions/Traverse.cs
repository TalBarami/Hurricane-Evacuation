using HurricaneEvacuation.SimulatorEnvironment.Impl.Settings;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Actions
{
    internal class Traverse : IAction
    {
        protected readonly IEdge Edge;
        protected readonly int CurrentPassengers;
        protected readonly IVertex CurrentPosition;
        public Traverse(IAgent agent, IEdge edge)
        {
            Edge = edge;
            Destination = edge.OtherV(agent.Position);
            CurrentPassengers = agent.Passengers;
            CurrentPosition = agent.Position;

            Cost = edge.Weight * (1 + CurrentPassengers * SettingsSingleton.Instance.SlowDown);
        }

        public double Cost { get; }
        public IVertex Destination { get; set; }

        public override string ToString()
        {
            return $"move from {CurrentPosition} to {Destination}";
        }
    }
}
