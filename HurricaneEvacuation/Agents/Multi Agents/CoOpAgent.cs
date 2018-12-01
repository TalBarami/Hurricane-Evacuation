using System.Linq;
using HurricaneEvacuation.Actions;
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

        protected override IAction CalculateMove(IState state)
        {
            var tree = new MaximaxTree(new Traverse(state, Id));
            tree.Root.PrintPretty("", true);
            return tree.Result.Action;
        }

        public override double MultiScore(IState state)
        {
            return state.Agents.Aggregate(0.0, (sum, agent) => sum + agent.Score);
        }

        public override double Heuristic(IState state)
        {
            return state.MultiAgents.Aggregate(0.0, (sum, next) => sum + next.Reachable(state)); ;
        }
    }
}
