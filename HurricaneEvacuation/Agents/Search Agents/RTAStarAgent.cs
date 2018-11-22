using System;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Agents.Search_Agents
{
    internal class RTAStarAgent : AbstractSearchAgent
    {
        private readonly int maximumExpands;
        private double traverseLength;

        public RTAStarAgent(int id, int position, int maximumExpands) : base(id, position)
        {
            this.maximumExpands = maximumExpands;
            traverseLength = 0;
        }

        public RTAStarAgent(int id, int position, int passengers, int peopleSaved, int searchExpansions,
            double traverseLength, int maximumExpands) : base(id, position, passengers, peopleSaved, searchExpansions)
        {
            this.traverseLength = traverseLength;
            this.maximumExpands = maximumExpands;
        }

        public RTAStarAgent(RTAStarAgent other) : this(other.Id, other.Position, other.Passengers, other.PeopleSaved,
            other.SearchExpansions, other.traverseLength, other.maximumExpands)
        {
        }

        public override ExpandTree CreateTree(IState state)
        {
            return new ExpandTree(new Traverse(state, Id), maximumExpands);
        }

        public override HeuristicResult Heuristic(IAction action)
        {
            if (!(action.Performer is RTAStarAgent performer)) throw new Exception("Heuristic for non-AI agent.");

            performer.traverseLength += action.Cost;
            return new SearchHeuristicResult(action, Unreachable(action) + performer.traverseLength);
        }

        public override IAgent Clone()
        {
            return new RTAStarAgent(this);
        }
    }
}
