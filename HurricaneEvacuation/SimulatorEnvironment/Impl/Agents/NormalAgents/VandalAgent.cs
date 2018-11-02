using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.NormalAgents
{
    internal class VandalAgent : AbstractAgent
    {
        private int InitialDelay { get; }
        private int timeToPlay;
        private bool blockNext;
        public VandalAgent(int id, ISettings settings, IVertex position, int initialDelay) : base(id, settings, position)
        {
            InitialDelay = initialDelay;
            timeToPlay = 0;
            blockNext = true;
        }

        protected override IAction PlayNext(double time)
        {
            if (timeToPlay < InitialDelay)
            {
                Console.WriteLine($"{Id} will start causing troubles in {InitialDelay}");
                timeToPlay++;
                return NoOperation();
            }
            var edges = Position.ValidEdges();
            if (edges.Count == 0)
            {
                Console.WriteLine($"{Id} couldn't do anything because there are no edges to block / travel.");
                return NoOperation();
            }

            Console.WriteLine($"{Id} is on the loose!");
            var nextEdge = FindMinimalEdge(edges);
            var nextAction = blockNext ? Block(nextEdge) : Traverse(nextEdge);

            timeToPlay = 0;
            blockNext = !blockNext;
            return nextAction;
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
