using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Agents.Multi_Agents
{
    internal class MinimaxTree
    {
        public TreeNode<MultiHeuristicResult> Root { get; }
        public MultiHeuristicResult Result { get; protected set; }

        public MinimaxTree(IAction initial)
        {
            Root = new TreeNode<MultiHeuristicResult>(new MultiHeuristicResult(initial, -1));
            Initialize();
            Result = Minimax();
        }
        private void Initialize()
        {
            var queue = new Queue<TreeNode<MultiHeuristicResult>>();
            queue.Enqueue(Root);
            while (queue.Any())
            {
                var node = queue.Dequeue();
                if (node.Depth == Constants.Cutoff)
                {
                    CutOff(node);
                }
                else
                {
                    var children = Expand(node);
                    children.ForEach(child => queue.Enqueue(child));
                }
            }

        }

        public List<TreeNode<MultiHeuristicResult>> Expand(TreeNode<MultiHeuristicResult> node)
        {
            var state = node.Data.Action.NewState;
            var player = state.Agents[state.CurrentAgent];

            var moves = player.PossibleMoves(state).Select(move => new MultiHeuristicResult(move, 0)).ToList();
            if (!moves.Any())
            {
                var s = node.Data.Action.NewState;
                var p = Root.Data.Action.Performer.Id;

                node.Data.Value = s.MultiAgents.First(ma => ma.Id == p).MultiScore(s);
            }
            return node.AddChildren(moves);
        }

        public void CutOff(TreeNode<MultiHeuristicResult> node)
        {
            var s = node.Data.Action.NewState;
            var p = Root.Data.Action.Performer.Id;
            var a = s.MultiAgents.First(ma => ma.Id == p);
            node.Data.Value = a.MultiScore(s) + a.Heuristic(s);
        }

        private MultiHeuristicResult Minimax()
        {
            Root.Data.Value = MaxValue(Root);
            return Root.Children
                .Aggregate(Root.Children[0], (min, next) => next.Data.Value > min.Data.Value ? next : min).Data;
        }

        private double MaxValue(TreeNode<MultiHeuristicResult> node)
        {
            if (!node.Children.Any())
            {
                return node.Data.Value;
            }

            node.Data.Value = double.MinValue;
            var value = node.Data.Value;
            foreach (var child in node.Children)
            {
                child.Data.Value = MinValue(child);
                var v = child.Data.Value;

                if (v > value) value = v;
                if (v >= node.Data.Beta)
                {
                    return value;
                }

                if (v > node.Data.Alpha)
                {
                    node.Data.Alpha = v;
                }
            }
            return value;
        }

        private double MinValue(TreeNode<MultiHeuristicResult> node)
        {
            if (!node.Children.Any())
            {
                return node.Data.Value;
            }

            var value = double.MaxValue;
            foreach (var child in node.Children)
            {
                child.Data.Value = MaxValue(child);
                var v = child.Data.Value;

                if (v < value) value = v;
                if (v <= node.Data.Alpha)
                {
                    return value;
                }
                if (v < node.Data.Beta)
                {
                    node.Data.Beta = v;
                }
            }
            return value;
        }
    }
}
