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
        protected readonly IEdge edge;
        protected readonly int currentPassengers;
        protected readonly IVertex currentPosition;
        public Traverse(IAgent agent, IEdge edge)
        {
            this.edge = edge;
            Destination = edge.OtherV(agent.Position);
            currentPassengers = agent.Passengers;
            currentPosition = agent.Position;

            Cost = edge.Weight * (1 + currentPassengers * SettingsSingleton.Instance.SlowDown);
        }

        public double Cost { get; }
        public IVertex Destination { get; set; }

        public override string ToString()
        {
            return $"move from {currentPosition} to {Destination}";
        }
    }
}
