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
