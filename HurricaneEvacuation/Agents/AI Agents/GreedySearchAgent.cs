using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Agents.AI_Agents
{
    internal class GreedySearchAgent : AbstractAIAgent
    {
        public GreedySearchAgent(int id, int position) : base(id, position)
        {
        }

        public GreedySearchAgent(int id, int position, int passengers, int peopleSaved, int searchExpansions) : base(id, position, passengers, peopleSaved, searchExpansions)
        {
        }

        public GreedySearchAgent(GreedySearchAgent other) : this(other.Id, other.Position, other.Passengers, other.PeopleSaved, other.SearchExpansions)
        {
        }

        public override ExpandTree CreateTree(IState state)
        {
            return new ExpandTree(new Traverse(state, Id));
        }

        public override HeuristicResult Heuristic(IAction action)
        {   
            return new HeuristicResult(action, action.NewState.GoalState ? 0 : Unreachable(action));
        }

        public override IAgent Clone()
        {
            return new GreedySearchAgent(this);
        }
    }
}
