using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.GraphComponents;

namespace HurricaneEvacuation.Agents.Search_Agents
{
    internal class ExpandTree
    {
        public SearchHeuristicResult Result { get; set; }
        public TreeNode<SearchHeuristicResult> Root { get; }
        public int Expands { get; set; }
        private readonly int maximumExpands;

        public ExpandTree(IAction initial, int maximumExpands)
        {
            Root = new TreeNode<SearchHeuristicResult>(new SearchHeuristicResult(initial, -1));
            Expands = 0;

            this.maximumExpands = maximumExpands;
            Initialize();
        }

        public ExpandTree(IAction initial) : this(initial, 5000)
        {
        }

        private void Initialize()
        {
            var state = Root.Data.Action.NewState;
            var node = Root;
            var possibleMoves = new List<TreeNode<SearchHeuristicResult>>();

            while (Expands <= maximumExpands && !state.GoalState)
            {
                Expands++;
                var results = Expand(node);
                possibleMoves.AddRange(results);
                node = SelectMinimal(possibleMoves);
                possibleMoves.Remove(node);

                state = node.Data.Action.NewState;
            }

            var pos = node.Data.Action.NewState.Graph.Vertex(node.Data.Action.Performer.Position);
            var path = new PathBuilder(pos, null, 0);
            while (node.Root != Root)
            {
                pos = node.Data.Action.NewState.Graph.Vertex(node.Root.Data.Action.Performer.Position);
                path = new PathBuilder(pos, path, 0);
                node = node.Root;
            }

            Result = node.Data;
        }

        private static List<TreeNode<SearchHeuristicResult>> Expand(TreeNode<SearchHeuristicResult> node)
        {
            var action = node.Data.Action;
            var state = node.Data.SimulatedState;

            if (!(action.Performer is AbstractSearchAgent agent)) throw new Exception("Expand with non-AI agent");

            var source = state.Graph.Vertex(agent.Position);

            var results = source.Neighbors
                .Select(v => agent.Heuristic(new Traverse(state, state.Clone(), agent.Id, v)) as SearchHeuristicResult)
                .ToList();
            return node.AddChildren(results);
        }

        private static TreeNode<SearchHeuristicResult> SelectMinimal(List<TreeNode<SearchHeuristicResult>> possibleMoves)
        {
            return possibleMoves.Aggregate(possibleMoves.First(), (min, next) => min.Data.Value> next.Data.Value ? next : min);
        }
    }
}
