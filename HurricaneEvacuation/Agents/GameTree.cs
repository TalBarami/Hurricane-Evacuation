using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Agents.Multi_Agents;
using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Agents
{
    abstract class GameTree
    {
        public TreeNode<MultiHeuristicResult> Root { get; }
        public MultiHeuristicResult Result { get; protected set; }

        protected GameTree(IAction initial)
        {
            Root = new TreeNode<MultiHeuristicResult>(new MultiHeuristicResult(initial, -1));
            Initialize();
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
            return node.AddChildren(moves);
        }

        public void CutOff(TreeNode<MultiHeuristicResult> node)
        {

            var data = node.Data;
            var state = data.Action.NewState;

            if (!(node.Data.Action.Performer is AbstractMultiAgent agent))
            {
                throw new Exception("Cutoff on non-AI agent");
            }

            data.Value = agent.MultiScore(state) + agent.SemiHeuristic(state);
        }
    }
}
