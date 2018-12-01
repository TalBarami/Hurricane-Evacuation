using System;
using System.Linq;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Agents.Multi_Agents
{
    class SemiCoOpAgent : AbstractMultiAgent
    {
        public SemiCoOpAgent(int id, int position) : base(id, position)
        {
        }

        public SemiCoOpAgent(int id, int position, int passengers, int peopleSaved) : base(id, position, passengers, peopleSaved)
        {
        }

        public SemiCoOpAgent(SemiCoOpAgent other) : this(other.Id, other.Position, other.Passengers,
            other.PeopleSaved)
        {
        }

        public override IAgent Clone()
        {
            return new SemiCoOpAgent(this);
        }

        protected override IAction CalculateMove(IState state)
        {
            var tree = new MinimaxTree(new Traverse(state, Id));
            tree.Root.PrintPretty("", true);
            return tree.Result.Action;
        }

        public override double MultiScore(IState state)
        {
            return state.MultiAgents.Where(a => a.Id != Id).Aggregate(0.0, (sum, agent) => sum + agent.Score) > 0
                ? Score + 0.1
                : Score;
        }

        public override double Heuristic(IState state)
        {
            var reachable = Reachable(state);
            var enemyReachable =
                state.MultiAgents.Where(a => a.Id != Id).Aggregate(0.0, (sum, next) => sum + next.Reachable(state));
            return enemyReachable > 0 
                ? reachable + 0.1
                : reachable;
        }
    }
}
