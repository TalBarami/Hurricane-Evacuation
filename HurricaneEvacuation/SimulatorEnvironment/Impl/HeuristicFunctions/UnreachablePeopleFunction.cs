using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions
{
    class UnreachablePeopleFunction : AbstractHeuristicFunction
    { 
        public override double Value(ISettings settings, IVertex source, double time)
        {
            var graph = settings.Graph;
            var deadline = settings.Deadline;

            var evacuationVertices = graph.Vertices.OfType<EvacuationVertex>();

            var unreachable = (from evacuationVertex in evacuationVertices
                let paths = GraphAlgorithms.Dijkstra(graph, evacuationVertex)
                let closestShelter = paths.Where(p => p.Source is ShelterVertex)
                    .Aggregate(paths[0],
                        (minWeight, newWeight) => newWeight.Weight < minWeight.Weight ? newWeight : minWeight)
                let sourceToEvacuation = paths.FirstOrDefault(p => p.Source.Equals(source))
                let evacuationsInPath = closestShelter.GetVertices().Union(sourceToEvacuation.GetVertices())
                    .OfType<EvacuationVertex>().Aggregate(0, (sum, v) => sum + v.PeopleCount)
                where sourceToEvacuation != null &&
                      time + GraphUtils.TraverseTime(sourceToEvacuation.Weight + closestShelter.Weight, evacuationsInPath, settings.SlowDown)
                      >= deadline
                select evacuationVertex).ToList();

            return time > deadline ? 0 : unreachable.Count * graph.Edges.Max(e => e.Weight) * 10;
        }
    }
}
