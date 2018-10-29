using System;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Settings;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Actions
{
    internal class Traverse : IAction
    {
        protected readonly IAgent Agent;
        protected readonly IEdge Edge;
        protected readonly int CurrentPassengers;
        protected readonly IVertex CurrentPosition;
        public Traverse(IAgent agent, IEdge edge)
        {
            Agent = agent;
            Edge = edge;
            Destination = edge.OtherV(agent.Position);
            CurrentPassengers = agent.Passengers;
            CurrentPosition = agent.Position;

            Cost = edge.Weight * (1 + CurrentPassengers * SettingsSingleton.Instance.SlowDown);
        }

        public virtual double Cost { get; }
        public virtual void Approve()
        {
            Console.WriteLine($"{Agent.Id} decided to {this} at cost {Cost}.");
            Agent.MoveTo(Destination);
        }

        public IVertex Destination { get; set; }

        public override string ToString()
        {
            return $"move from {CurrentPosition} to {Destination}";
        }
    }
}
