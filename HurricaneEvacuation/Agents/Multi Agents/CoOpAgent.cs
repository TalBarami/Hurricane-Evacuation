using System.Linq;
using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Agents.Multi_Agents
{
    class CoOpAgent : AbstractMultiAgent
    {
        public CoOpAgent(int id, int position) : base(id, position)
        {
        }

        public CoOpAgent(int id, int position, int passengers, int peopleSaved) : base(id, position, passengers, peopleSaved)
        {
        }

        public CoOpAgent(CoOpAgent other) : this(other.Id, other.Position, other.Passengers,
            other.PeopleSaved)
        {
        }

        public override IAgent Clone()
        {
            return new CoOpAgent(this);
        }

        public override double MultiScore(IState state)
        {
            return state.Agents.Aggregate(0.0, (sum, agent) => sum + agent.Score);
        }
    }
}
