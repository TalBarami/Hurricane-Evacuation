using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.GraphComponents;

namespace HurricaneEvacuation.Agents.AI_Agents
{
    internal class ExpandTree
    {
        public HeuristicResult Result { get; set; }
        public ExpandNode<HeuristicResult> Root { get; }
        public int Expands { get; set; }
        private readonly int maximumExpands;

        public ExpandTree(IAction initial, int maximumExpands)
        {
            Root = new ExpandNode<HeuristicResult>(new HeuristicResult(initial, -1));
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
            var possibleMoves = new List<ExpandNode<HeuristicResult>>();

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

        private static List<ExpandNode<HeuristicResult>> Expand(ExpandNode<HeuristicResult> node)
        {
            var action = node.Data.Action;
            var state = node.Data.SimulatedState;

            if (!(action.Performer is AbstractAIAgent agent)) throw new Exception("Expand with non-AI agent");

            var source = state.Graph.Vertex(agent.Position);

            var results = source.Neighbors
                .Select(v => agent.Heuristic(new Traverse(state, state.Clone(), agent.Id, v)))
                .ToList();
            results.ForEach(r => node.AddChild(r));
            return node.Children;
        }

        private static ExpandNode<HeuristicResult> SelectMinimal(List<ExpandNode<HeuristicResult>> possibleMoves)
        {
            return possibleMoves.Aggregate(possibleMoves.First(), (min, next) => min.Data.Value> next.Data.Value ? next : min);
        }


        internal class ExpandNode<T>
        {
            public T Data { get; }
            public ExpandNode<T> Root { get; }
            public List<ExpandNode<T>> Children { get; }

            public ExpandNode(T data, ExpandNode<T> root = null)
            {
                Data = data;
                Root = root;
                Children = new List<ExpandNode<T>>();
            }

            public ExpandNode<T> AddChild(T child)
            {
                var node = new ExpandNode<T>(child, this);
                Children.Add(node);
                return node;
            }
            public ExpandNode<T> GetChild(T child)
            {
                return Children.First(c => c.Data.Equals(child));
            }

            public bool HasChild(T child)
            {
                return Children.Any(node => node.Data.Equals(child));
            }

            public override string ToString()
            {
                return Data.ToString();
            }
        }
    }
}
