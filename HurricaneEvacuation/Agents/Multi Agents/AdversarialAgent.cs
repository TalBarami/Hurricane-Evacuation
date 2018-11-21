using System.Linq;
using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Agents.Multi_Agents
{
    class AdversarialAgent : AbstractMultiAgent
    {
        public AdversarialAgent(int id, int position) : base(id, position)
        {
        }

        public AdversarialAgent(int id, int position, int passengers, int peopleSaved) : base(id, position, passengers, peopleSaved)
        {
        }

        public AdversarialAgent(AdversarialAgent other) : this(other.Id, other.Position, other.Passengers,
            other.PeopleSaved)
        {
        }

        public override IAgent Clone()
        {
            return new AdversarialAgent(this);
        }
        public override double MultiScore(IState state)
        {
            return 2 * Score - state.Agents.Aggregate(0.0, (sum, agent) => sum + agent.Score);
        }
    }
}
