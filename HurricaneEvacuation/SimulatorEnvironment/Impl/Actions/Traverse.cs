using System;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Actions
{
    internal class Traverse : IAction
    {
        public IVertex Destination { get; }
        public readonly IEdge Edge;
        protected readonly IAgent Agent;
        protected readonly int CurrentPassengers;
        protected readonly IVertex CurrentPosition;
        public Traverse(IAgent agent, IEdge edge, double slowDown)
        {
            Agent = agent;
            Edge = edge;
            Destination = Edge.OtherV(Agent.Position);
            CurrentPassengers = Agent.Passengers;
            CurrentPosition = Agent.Position;

            Cost = GraphUtils.TraverseTime(edge.Weight, CurrentPassengers, slowDown);
        }

        public double Cost { get; }
        public void Approve()
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
