using System;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Actions
{
    internal class NoOperation : IAction
    {
        public IVertex Destination { get; }
        protected readonly IAgent Agent;

        public NoOperation(IAgent agent, IVertex destination)
        {
            Agent = agent;
            Destination = destination;
            Cost = 1;
        }

        public double Cost { get; }
        public void Approve()
        {
            Console.WriteLine($"{Agent} decided to {this}");
        }

        public override string ToString()
        {
            return $"do nothing at cost {Cost}";
        }
    }
}
