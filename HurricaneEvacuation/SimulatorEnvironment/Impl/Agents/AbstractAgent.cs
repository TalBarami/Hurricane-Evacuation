using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    abstract class AbstractAgent : IAgent
    {
        public IVertex Position { get; set; }
        public IVertex Goal { get; set; }
        public int TicksLeft { get; set; }
        public int Passengers { get; set; }

        protected AbstractAgent(IVertex position)
        {
            Position = position;
        }

        public abstract IAction PerformStep(IGraph world);

        public void Visit(EvacuationVertex v)
        {
            Passengers += v.PeopleCount;
            v.PeopleCount = 0;
        }

        public void Visit(ShelterVertex v)
        {
            Passengers = 0;
        }

        public void Visit(EmptyVertex v)
        {
        }
    }
}
