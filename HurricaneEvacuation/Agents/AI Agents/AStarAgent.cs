using System;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Agents.AI_Agents
{
    internal class AStarAgent : AbstractAIAgent
    {
        private double traverseLength;

        public AStarAgent(int id, int position) : base(id, position)
        {
            traverseLength = 0;
        }

        public AStarAgent(int id, int position, int passengers, int peopleSaved, int searchExpansions, double traverseLength) : base(id, position, passengers, peopleSaved, searchExpansions)
        {
            this.traverseLength = traverseLength;
        }

        public AStarAgent(AStarAgent other) : this(other.Id, other.Position, other.Passengers, other.PeopleSaved, other.SearchExpansions, other.traverseLength)
        {

        }

        public override IAgent Clone()
        {
            return new AStarAgent(this);
        }

        public override ExpandTree CreateTree(IState state)
        {
            return new ExpandTree(new Traverse(state, Id));
        }

        public override HeuristicResult Heuristic(IAction action)
        {
            if (!(action.Performer is AStarAgent performer)) throw new Exception("Heuristic for non-AI agent.");

            performer.traverseLength += action.Cost;
            return new HeuristicResult(action, Unreachable(action) + performer.traverseLength);
        }

    }
}
