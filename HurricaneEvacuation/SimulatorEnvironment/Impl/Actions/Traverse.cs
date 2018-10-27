using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Settings;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Actions
{
    internal class Traverse : IAction
    {
        private readonly IEdge edge;
        private readonly int currentPassengers;
        private readonly IVertex currentPosition;
        public Traverse(IVertex destination, IAgent agent, IEdge edge)
        {
            Destination = destination;
            this.edge = edge;
            currentPassengers = agent.Passengers;
            currentPosition = agent.Position;
            
        }

        public double Cost => edge.Weight * (1 + currentPassengers * SettingsSingleton.Instance.SlowDown);

        public IVertex Destination { get; set; }

        public override string ToString()
        {
            return $"move from {currentPosition} to {Destination}";
        }
    }
}
