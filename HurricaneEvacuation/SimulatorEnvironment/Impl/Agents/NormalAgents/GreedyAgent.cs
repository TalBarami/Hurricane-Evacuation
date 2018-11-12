using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.NormalAgents
{
    internal class GreedyAgent : VehicleAgent
    {
        public GreedyAgent(int id, ISettings settings, IVertex position) : base(id, settings, position)
        {
        }

        protected override IAction PlayNext(double time)
        {
            var state = new State(Settings, null, Passengers, time);
            var paths = GraphAlgorithms.Dijkstra(Position, state);

            var selectedPath = Passengers > 0 ? FindShelter(paths) : FindPeople(paths);

            if (selectedPath.Next == null)
            {
                return NoOperation();
            }

            var nestedPath = selectedPath;
            while (nestedPath.Next != null && nestedPath.Next.Source != Position)
            {
                nestedPath = nestedPath.Next;
            }

            var dst = nestedPath.Source;
            return Traverse(dst.ValidEdges().First(e => e.OtherV(dst) == Position));
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
