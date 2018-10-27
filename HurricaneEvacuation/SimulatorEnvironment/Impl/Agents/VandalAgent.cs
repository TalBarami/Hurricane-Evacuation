using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    internal class VandalAgent : AbstractAgent
    {
        private int InitialDelay { get; set; }
        public VandalAgent(int id, IVertex position, int initialDelay) : base(position)
        {
            Id = $"VandalAgent{id}";
            InitialDelay = initialDelay;
        }

        public override IAction PlayNext()
        {
            if (InitialDelay > 0)
            {
                Console.WriteLine($"{Id} will start causing troubles in {InitialDelay}");
                InitialDelay--;
                return new NoOperation(Position);
            }
            var edges = Position.ValidEdges();
            if (edges.Count == 0)
            {
                return new NoOperation(Position);
            }

            Console.WriteLine($"{Id} is on the loose!");
            var minimal = edges.Aggregate(edges[0], (minEdge, newEdge) =>
                (newEdge.CompareTo(minEdge) < 0) ||
                (newEdge.CompareTo(minEdge) == 0 && newEdge.OtherV(Position).CompareTo(minEdge.OtherV(Position)) < 0)
                    ? newEdge
                    : minEdge);
            minimal.Blocked = true;
            Console.WriteLine($"{Id} blocked the path: {minimal}");

            return new Traverse(minimal.OtherV(Position), this, minimal);
        }
    }
}
