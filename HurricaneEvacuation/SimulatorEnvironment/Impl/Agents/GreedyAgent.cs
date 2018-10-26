using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    internal class GreedyAgent : AbstractAgent
    {
        public GreedyAgent(int id, IVertex position) : base(position)
        {
            Id = $"GreedyAgent{id}";
        }

        public override IAction PlayNext(IGraph world)
        {
            // If carry - find shelter. Else, or if can't reach shelter - find people, Else or if can't reach people, nop.
            var paths = GraphAlgorithms.Dijkstra(world, Position);

            IPath selectedPath = Passengers > 0 ? FindShelter(paths) : FindPeople(paths);

            if (selectedPath.Next == null)
            {
                return new NoOperation(Position);
            }

            var nestedPath = selectedPath;
            while (nestedPath.Next != null && nestedPath.Next.Source != Position)
            {
                nestedPath = nestedPath.Next;
            }

            var dst = nestedPath.Source;
            return new Traverse(dst, this, dst.ValidEdges().First(e => e.OtherV(dst) == Position));
        }

        private IPath FindShelter(IList<IPath> paths)
        {
            Console.WriteLine($"{Id} is looking for shelter to drop his passengers.");
            var path = paths.Where(p => p.Source is ShelterVertex)
                .DefaultIfEmpty(paths[0])
                .Aggregate((curMin, newMin) => newMin.Weight < curMin.Weight ? newMin : curMin);
            if (path.Next != null) return path;

            Console.WriteLine($"{Id} couldn't find a shelter.");
            return FindPeople(paths);
        }

        private IPath FindPeople(IList<IPath> paths)
        {
            Console.WriteLine($"{Id} is looking for people to evacuate.");
            var path = paths.Where(p => p.Source is EvacuationVertex ev && ev.PeopleCount > 0)
                .DefaultIfEmpty(paths[0])
                .Aggregate((curMin, newMin) => newMin.Weight < curMin.Weight ? newMin : curMin);
            if (path.Next == null)
            {
                Console.WriteLine($"{Id} couldn't find people to evacuate.");
            }

            return path;
        }
    }
}
