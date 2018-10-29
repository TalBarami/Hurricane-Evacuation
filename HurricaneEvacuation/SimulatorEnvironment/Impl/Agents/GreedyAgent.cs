using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Settings;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    internal class GreedyAgent : VehicleAgent
    {
        public GreedyAgent(int id, IVertex position) : base(id, position)
        {
        }

        protected override IAction PlayNext()
        {
            var world = SettingsSingleton.Instance;
            var paths = GraphAlgorithms.Dijkstra(world.Graph, Position);

            var selectedPath = Passengers > 0 ? FindShelter(paths) : FindPeople(paths);

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
            return new Traverse(this, dst.ValidEdges().First(e => e.OtherV(dst) == Position));
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
