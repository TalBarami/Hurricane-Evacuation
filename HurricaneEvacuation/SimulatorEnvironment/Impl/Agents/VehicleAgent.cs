using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    abstract class VehicleAgent : AbstractAgent
    {
        protected VehicleAgent(int id, IVertex position) : base(id, position)
        {
        }

        public override void Visit(EvacuationVertex v)
        {
            if (v.PeopleCount <= 0) return;
            Passengers += v.PeopleCount;
            Console.WriteLine($"{Id} picked {v.PeopleCount} passengers and now carries {Passengers} passengers.");
            v.PeopleCount = 0;
        }

        public override void Visit(ShelterVertex v)
        {
            if (Passengers <= 0) return;
            PeopleSaved += Passengers;
            Console.WriteLine($"{Id} dropped {Passengers} passengers in a shelter, saved total of {PeopleSaved} passengers");
            Passengers = 0;
        }
    }
}
