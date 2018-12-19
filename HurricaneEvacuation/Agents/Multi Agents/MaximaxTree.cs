using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Agents.Multi_Agents
{
    class MaximaxTree
    {
        public TreeNode<MultiValuesHeuristicResult> Root { get; }
        public MultiValuesHeuristicResult Result { get; protected set; }

        public MaximaxTree(IAction initial)
        {
            Root = new TreeNode<MultiValuesHeuristicResult>(new MultiValuesHeuristicResult(initial, new double[0]));
            Initialize();
            Result = Maximax();
        }

        private MultiValuesHeuristicResult Maximax()
        {
            Root.Data.Values = MaxValue(Root);
            
            var agent = Root.Data.Action.Performer.Id;

            return Root.Children
                .Aggregate(Root.Children[0], (min, next) => next.Data.Values[agent] > min.Data.Values[agent] ? next : min).Data;
        }

        private double[] MaxValue(TreeNode<MultiValuesHeuristicResult> node)
        {
            if (!node.Children.Any())
            {
                return node.Data.Values;
            }

            var state = node.Data.Action.NewState;
            var agent = state.CurrentAgent;

            node.Data.Values = Enumerable.Repeat(double.MinValue, state.Agents.Count).ToArray();
            var value = node.Data.Values;
            foreach (var child in node.Children)
            {
                child.Data.Values = MaxValue(child);
                var v = child.Data.Values;
                if (v[agent] > value[agent]) value = v;
            }
            return value;
        }

        private void Initialize()
        {
            var queue = new Queue<TreeNode<MultiValuesHeuristicResult>>();
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

        public List<TreeNode<MultiValuesHeuristicResult>> Expand(TreeNode<MultiValuesHeuristicResult> node)
        {
            var state = node.Data.Action.NewState;
            var player = state.Agents[state.CurrentAgent];

            var moves = player.PossibleMoves(state).Select(move => new MultiValuesHeuristicResult(move, new double[0])).ToList();
            if (state.GoalState || !moves.Any())
            {
                var s = node.Data.Action.NewState;
                node.Data.Values = s.MultiAgents.Select(a => a.MultiScore(s)).ToArray();
                return node.AddChildren(new List<MultiValuesHeuristicResult>());
            }

            return node.AddChildren(moves);
        }

        public void CutOff(TreeNode<MultiValuesHeuristicResult> node)
        {
            var s = node.Data.Action.NewState;
            node.Data.Values = s.MultiAgents.Select(a => a.MultiScore(s) + a.Heuristic(s)).ToArray();
        }
    }
}


/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Agents.Multi_Agents
{
    class MaximaxTree : GameTree
    {
        public MaximaxTree(IAction initial) : base(initial)
        {
            Result = Maximax();
        }

        private MultiHeuristicResult Maximax()
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
                child.Data.Value = MaxValue(child);
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
    }
}
*/
