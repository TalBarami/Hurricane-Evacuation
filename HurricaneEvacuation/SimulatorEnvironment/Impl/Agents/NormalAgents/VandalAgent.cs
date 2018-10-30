using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.NormalAgents
{
    internal class VandalAgent : AbstractAgent
    {
        private int InitialDelay { get; set; }
        public VandalAgent(int id, ISettings settings, IVertex position, int initialDelay) : base(id, settings, position)
        {
            InitialDelay = initialDelay;
        }

        protected override IAction PlayNext(double time)
        {
            if (InitialDelay > 0)
            {
                Console.WriteLine($"{Id} will start causing troubles in {InitialDelay}");
                InitialDelay--;
                return new NoOperation(this, Position);
            }
            var edges = Position.ValidEdges();
            if (edges.Count == 0)
            {
                return new NoOperation(this, Position);
            }

            Console.WriteLine($"{Id} is on the loose!");
            var blockEdge = FindMinimalEdge(edges);
            edges.Remove(blockEdge);
            var destinationEdge = FindMinimalEdge(edges);

            return new BlockingTraverse(this, destinationEdge, blockEdge, Settings.SlowDown);
        }

        private IEdge FindMinimalEdge(IList<IEdge> edges)
        {
            return edges.Aggregate(edges[0], (minEdge, newEdge) =>
                (newEdge.CompareTo(minEdge) < 0) ||
                (newEdge.CompareTo(minEdge) == 0 && newEdge.OtherV(Position).CompareTo(minEdge.OtherV(Position)) < 0)
                    ? newEdge
                    : minEdge);
        }
    }
}
