using System;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Settings;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Actions
{
    internal class Traverse : IAction
    {
        public IVertex Destination { get; }
        protected readonly IAgent Agent;
        protected readonly IEdge Edge;
        protected readonly int CurrentPassengers;
        protected readonly IVertex CurrentPosition;
        public Traverse(IAgent agent, IEdge edge, double slowDown)
        {
            Agent = agent;
            Edge = edge;
            Destination = edge.OtherV(agent.Position);
            CurrentPassengers = agent.Passengers;
            CurrentPosition = agent.Position;

            Cost = GraphUtils.TraverseTime(edge.Weight, CurrentPassengers, slowDown);
        }

        public virtual double Cost { get; }
        public virtual void Approve()
        {
            Console.WriteLine($"{Agent.Id} decided to {this}.");
            Agent.MoveTo(Destination);
        }

        public override string ToString()
        {
            return $"move from {CurrentPosition} to {Destination} at cost {Cost}";
        }
    }
}
