using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Actions
{
    internal class Traverse : IAction
    {
        private const double K = 0.15;

        private readonly IAgent agent;
        private readonly IEdge edge;
        public Traverse(IVertex destination, IAgent agent, IEdge edge)
        {
            Destination = destination;
            this.agent = agent;
            this.edge = edge;
        }

        public IVertex Destination { get; set; }

        public double Cost()
        {
            return edge.Weight * (1 + agent.Passengers * K);
        }

        public override string ToString()
        {
            return $"move from {agent.Position} to {Destination}";
        }
    }
}
