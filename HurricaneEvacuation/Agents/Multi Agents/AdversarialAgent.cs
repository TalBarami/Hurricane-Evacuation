using System;
using System.Linq;
using HurricaneEvacuation.Actions;
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
        protected override IAction CalculateMove(IState state)
        {
            var tree = new MinimaxTree(new Traverse(state, Id));
            tree.Root.PrintPretty("", true);
            return tree.Result.Action;
        }

        public override double MultiScore(IState state)
        {
            return Score - state.MultiAgents.Where(a => a.Id != Id).Aggregate(0.0, (sum, agent) => sum + agent.Score);
        }

        public override double Heuristic(IState state)
        {
            var reachable = Reachable(state);
            var enemyReachable =
                state.MultiAgents.Where(a => a.Id != Id).Aggregate(0.0, (sum, next) => sum + next.Reachable(state));
            return reachable - enemyReachable;
        }
    }
}
