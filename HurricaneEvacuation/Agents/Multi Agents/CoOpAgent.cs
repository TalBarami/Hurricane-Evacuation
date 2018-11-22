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
            return tree.Result.Action;
        }

        public override HeuristicResult Heuristic(IAction action)
        {
            throw new System.NotImplementedException();
        }

        public override double MultiScore(IState state)
        {
            return state.Agents.Aggregate(0.0, (sum, agent) => sum + agent.Score);
        }

        public override double SemiHeuristic(IState state)
        {
            var reachable = Reachable(state);
            var partnerReachable =
                state.MultiAgents.Where(a => a.Id != Id).Aggregate(0.0, (sum, next) => sum + next.Reachable(state));
            return reachable + partnerReachable;
        }
    }
}
