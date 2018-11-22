using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurricaneEvacuation.Agents
{
    class TreeNode<T>
    {
        public T Data { get; }
        public TreeNode<T> Root { get; }
        public int Depth { get; }
        public List<TreeNode<T>> Children { get; }

        public TreeNode(T data, TreeNode<T> root = null)
        {
            Data = data;
            Root = root;
            Depth = Root?.Depth + 1 ?? 0;
            Children = new List<TreeNode<T>>();
        }

        public TreeNode<T> AddChild(T child)
        {
            var node = new TreeNode<T>(child, this);
            Children.Add(node);
            return node;
        }

        public List<TreeNode<T>> AddChildren(List<T> children)
        {
            var nodes = children.Select(c => new TreeNode<T>(c, this)).ToList();
            Children.AddRange(nodes);
            return nodes;
        }
        public override string ToString()
        {
            return Data.ToString();
        }

        public void PrintPretty(string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
                indent += "  ";
            }
            else
            {
                Console.Write("|-");
                indent += "| ";
            }
            Console.WriteLine(this);

            for (var i = 0; i < Children.Count; i++)
                Children[i].PrintPretty(indent, i == Children.Count - 1);
        }
    }
}
