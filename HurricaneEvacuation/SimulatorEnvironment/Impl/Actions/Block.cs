using System;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Actions
{
    class Block : IAction
    {
        public IVertex Destination { get; }
        protected readonly IAgent Agent;
        protected readonly IEdge Edge;

        public Block(IAgent agent, IEdge edge)
        {
            Agent = agent;
            Edge = edge;
            Destination = Agent.Position;
            Cost = 1;
        }

        public double Cost { get; }
        public void Approve()
        {
            Console.WriteLine($"{Agent.Id} decided to {this}");
            Edge.Blocked = true;
        }

        public override string ToString()
        {
            return $"block the path to {Edge} at cost {Cost}";
        }
    }
}
