using System;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Actions
{
    internal class NoOperation : IAction
    {
        public IVertex Destination { get; }
        protected readonly IAgent Agent;

        public NoOperation(IAgent agent)
        {
            Agent = agent;
            Destination = agent.Position;
            Cost = 1;
        }

        public double Cost { get; }
        public void Approve()
        {
            Console.WriteLine($"{Agent.Id} decided to {this}");
        }

        public override string ToString()
        {
            return $"do nothing at cost {Cost}";
        }
    }
}
