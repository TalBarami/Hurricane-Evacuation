using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions
{
    class UnreachablePeopleFunction : AbstractHeuristicFunction
    { 
        public override double Value(IGraph graph, IVertex source, double time, double deadline)
        {
            var evacuationVertices = graph.Vertices.Where(v => v is EvacuationVertex);

            var unreachable = (from evacuationVertex in evacuationVertices
                let paths = GraphAlgorithms.Dijkstra(graph, evacuationVertex)
                let closestShelter = paths.Where(p => p.Source is ShelterVertex)
                    .Aggregate(paths[0], (minWeight, newWeight) => newWeight.Weight < minWeight.Weight ? newWeight : minWeight)
                let sourceToEvacuation = paths.FirstOrDefault(p => p.Source.Equals(source))
                where sourceToEvacuation != null && time + sourceToEvacuation.Weight + closestShelter.Weight >= deadline
                select evacuationVertex).ToList();

            return time > deadline ? 0 : unreachable.Count * graph.Edges.Max(e => e.Weight) * 10;
        }
    }
}
