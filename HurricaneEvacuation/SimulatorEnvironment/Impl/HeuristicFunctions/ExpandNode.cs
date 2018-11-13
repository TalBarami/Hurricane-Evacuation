using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions
{
    class ExpandNode
    {
        public HeuristicResult result;
        public ExpandNode root;
        public IList<ExpandNode> children;
        public IList<IVertex> Visited
        {
            get
            {
                var visited = new List<IVertex>() {result.Action.Destination};
                var node = root;

                while (node != null)
                {
                    visited.Add(node.result.Action.Destination);
                    node = node.root;
                }

                return visited;
            }
        }

        public ExpandNode(HeuristicResult result, ExpandNode root = null)
        {
            this.result = result;
            this.root = root;
            children = new List<ExpandNode>();
        }

        public ExpandNode AddChild(HeuristicResult child)
        {
            var node = new ExpandNode(child, this);
            children.Add(node);
            return node;
        }

        public ExpandNode GetChild(HeuristicResult child)
        {
            return children.First(c => c.result == child);
        }

        public bool HasChild(HeuristicResult child)
        {
            return children.Any(en => en.result == child);
        }
    }
}
