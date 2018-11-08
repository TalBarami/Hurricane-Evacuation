using System;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.NormalAgents
{
    abstract class VehicleAgent : AbstractAgent
    {
        public override double Score => PeopleSaved;
        protected VehicleAgent(int id, ISettings settings, IVertex position) : base(id, settings, position)
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
