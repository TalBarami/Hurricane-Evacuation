using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Agents.Multi_Agents
{
    internal class MinimaxTree
    {
        public MinimaxNode Root { get; }
        public MinimaxTree(IAction action)
        {
            Root = new MaxNode(action, null);
        }

        public IAction SelectedAction => Root.ChildNodes.Aggregate(Root.ChildNodes[0], (maxNode, currentNode) =>
            currentNode.Value > maxNode.Value ? currentNode : maxNode).Action;

        internal abstract class MinimaxNode
        {
            public IAction Action{ get; }
            public double Value { get; set; }
            public double Alpha { get; set; }
            public double Beta { get; set; }
            public List<MinimaxNode> ChildNodes { get; }

            protected readonly MinimaxNode Root;
            protected readonly int Depth;

            protected MinimaxNode(IAction action, MinimaxNode root)
            {
                if (root == null)
                {
                    Depth = 0;
                }
                else
                {
                    Root = root;
                    Depth = root.Depth + 1;
                }
                Action = action;
                ChildNodes = new List<MinimaxNode>();
            }

            protected List<IAction> PossibleMoves()
            {
                var state = Action.NewState;
                var agent = state.Agents[state.CurrentAgent];
                var position = state.Graph.Vertices[agent.Position];

                var possibleMoves = new List<IAction> {new NoOp(state, state.Clone(), agent.Id)};
                possibleMoves.AddRange(position.Neighbors.Select(v => new Traverse(Action.NewState, Action.NewState.Clone(), agent.Id, v))
                    .ToList());
                return possibleMoves;
            }

            protected void Cutoff()
            {
                foreach (var action in PossibleMoves())
                {
                    if (action.NewState.GoalState)
                    {
                        ChildNodes.Add(new LeafNode(action, this));
                    }
                    else
                    {
                        ChildNodes.Add(new CutoffNode(action, this));
                    }
                }
            }

            public override string ToString()
            {
                return $"({Value},{Alpha}{Beta})->{Action}";
            }
        }
        class MinNode : MinimaxNode
        {
            public MinNode(IAction action, MinimaxNode root) : base(action, root)
            {
                if (Depth == Constants.Cutoff)
                {
                    Cutoff();
                }
                else
                {
                    Expand();
                }
                Value = MinValue();
            }

            private void Expand()
            {
                var possibleMoves = PossibleMoves();
                foreach (var action in possibleMoves)
                {
                    if (action.NewState.GoalState)
                    {
                        ChildNodes.Add(new LeafNode(action, this));
                    }
                    else
                    {
                        ChildNodes.Add(new MaxNode(action, this));
                    }
                }
            }

            private double MinValue()
            {
                var value = double.MaxValue;
                foreach (var child in ChildNodes)
                {
                    var childValue = child.Value;
                    if (childValue < value) value = childValue;
                    if (childValue <= Alpha) return value;
                    if (childValue < Beta) Beta = childValue;
                }

                return value;
            }
        }

        class MaxNode : MinimaxNode
        {
            
            public MaxNode(IAction action, MinimaxNode root) : base(action, root)
            {
                if (Depth == Constants.Cutoff)
                {
                    Cutoff();
                }
                else
                {
                    Expand();
                }
                Value = MaxValue();
            }

            private void Expand()
            {
                var possibleMoves = PossibleMoves();
                foreach (var action in possibleMoves)
                {
                    if (action.NewState.GoalState)
                    {
                        ChildNodes.Add(new LeafNode(action, this));
                    }
                    else
                    {
                        // TODO: Temporary - maybe there is no need of min nodes.
                        ChildNodes.Add(new MaxNode(action, this));
                    }
                }
            }

            private double MaxValue()
            {
                var value = double.MinValue;
                foreach (var child in ChildNodes)
                {
                    var childValue = child.Value;
                    if (childValue > value) value = childValue;
                    if (childValue >= Beta) return value;
                    if (childValue > Alpha) Alpha = childValue;
                }

                return value;
            }
        }

        class LeafNode : MinimaxNode
        {
            public LeafNode(IAction action, MinimaxNode root) : base(action, root)
            {
                if (!(action.Performer is AbstractMultiAgent agent))
                {
                    throw new Exception("Cutoff on non-AI agent");
                }

                Value = agent.MultiScore(action.NewState);
            }
        }

        class CutoffNode : MinimaxNode
        {
            public CutoffNode(IAction action, MinimaxNode root) : base(action, root)
            {
                if (!(action.Performer is AbstractMultiAgent agent))
                {
                    throw new Exception("Cutoff on non-AI agent");
                }

                Value = agent.Reachable(action);
            }
        }
    }
}
