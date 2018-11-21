using HurricaneEvacuation.Environment;
using HurricaneEvacuation.GraphComponents.Vertices;

namespace HurricaneEvacuation.Agents
{
    public abstract class AbstractHelpfulAgent : AbstractAgent
    {
        public int Passengers { get; set; }
        public int PeopleSaved { get; set; }
        public override double Score => PeopleSaved;
        protected AbstractHelpfulAgent(int id, int position) : this(id, position, 0, 0)
        {
        }
        protected AbstractHelpfulAgent(int id, int position, int passengers, int peopleSaved) : base(id, position)
        {
            Passengers = passengers;
            PeopleSaved = peopleSaved;
        }

        public override double CalculateWeight(int edgeWeight)
        {
            return edgeWeight * (1 + Passengers * Constants.SlowDown);
        }

        public override string Visit(IVertex v)
        {
            switch (v)
            {
                case ShelterVertex _:
                    var saved = Passengers;
                    Passengers = 0;
                    PeopleSaved += saved;
                    return saved > 0 ? $"save {saved} passengers." : "";
                case EvacuationVertex ev:
                    var picked = ev.PeopleCount;
                    ev.PeopleCount = 0;
                    Passengers = Passengers + picked;
                    return $"pick {picked} passengers.";
                default:
                    return "";
            }
        }

        public override string ToString()
        {
            return $"{Name}(Position {Position}, Passengers {Passengers}, Saved {PeopleSaved})";
        }
    }
}
