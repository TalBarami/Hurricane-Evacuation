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
        public int Passengers { get; set; }
        public abstract IAction PerformStep();

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
