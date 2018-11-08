using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.NormalAgents
{
    internal class VandalAgent : AbstractAgent
    {
        public override double Score => 0;
        private int InitialDelay { get; }
        private int timeToPlay;
        private bool blockNext;

        public IPath Path;
        public Dictionary<IEdge, double> BlockTimes;

        public VandalAgent(int id, ISettings settings, IVertex position, int initialDelay) : base(id, settings, position)
        {
            InitialDelay = initialDelay;
            timeToPlay = 0;
            blockNext = true;

            Path = null;
            BlockTimes = null;
        }

        protected override IAction PlayNext(double time)
        {
            if (Path == null || BlockTimes == null)
            {
                (Path, BlockTimes) = GetPath(time);
            }
            if (timeToPlay < InitialDelay)
            {
                Console.WriteLine($"{Id} will start causing troubles in {InitialDelay - timeToPlay}");
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
            var nextEdge = FindMinimalEdge(Position, edges);
            var nextAction = blockNext ? Block(nextEdge) : Traverse(nextEdge);

            timeToPlay = 0;
            blockNext = !blockNext;
            return nextAction;
        }

        private static IEdge FindMinimalEdge(IVertex position, IList<IEdge> edges)
        {
            return edges.Aggregate(edges[0], (minEdge, newEdge) =>
                (newEdge.CompareTo(minEdge) < 0) ||
                (newEdge.CompareTo(minEdge) == 0 && newEdge.OtherV(position).CompareTo(minEdge.OtherV(position)) < 0)
                    ? newEdge
                    : minEdge);
        }

        public (IPath, Dictionary<IEdge, double>) GetPath(double time)
        {
            var pos = Position;
            var blockTime = InitialDelay - timeToPlay;
            var block = blockNext;

            var blockTimes = new Dictionary<IEdge, double>();
            IPath path = new Path(pos, null, 0);

            do
            {
                var edges = pos.ValidEdges().Where(e => !blockTimes.Keys.Contains(e)).ToList();
                if (edges.Count == 0)
                {
                    break;
                }
                var next = FindMinimalEdge(pos, edges);
                if (block)
                {
                    blockTimes.Add(next, blockTime);
                    blockTime += 1;
                }
                else
                {
                    blockTime += next.Weight;
                    pos = next.OtherV(pos);
                    path = new Path(pos, path, path.Weight + next.Weight);
                }

                block = !block;
                blockTime += InitialDelay;
            } while (true);


            return (path, blockTimes);
        }
    }
}
