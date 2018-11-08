using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.NormalAgents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions
{
    class UnreachablePeopleFunction : AbstractHeuristicFunction
    { 
        public override HeuristicResult Apply(IAgent agent, Traverse move, double time)
        {
            SearchExpansions++;
            var settings = agent.Settings;

            var graph = settings.Graph;
            var deadline = settings.Deadline;
            var totalTime = move.Cost + time;

            var vandalAgents = settings.Agents.OfType<VandalAgent>();
            if (vandalAgents.Any(a => a.BlockTimes.ContainsKey(move.Edge) && a.BlockTimes[move.Edge] <= time))
            {
                return new HeuristicResult(move, int.MaxValue, agent.Passengers, time, deadline);
            }

            var evacuationVertices = graph.Vertices.OfType<EvacuationVertex>().Where(v => v.PeopleCount > 0).ToList();
            var unreachable = new List<EvacuationVertex>();

            var reachedAny = false;
            foreach (var evacuationVertex in evacuationVertices)
            {
                var traversePassengers = agent.Passengers;
                if (evacuationVertex == move.Destination)
                {
                    traversePassengers += evacuationVertex.PeopleCount;
                }

                var evacuationPaths = GraphAlgorithms.Dijkstra(graph, evacuationVertex);
                var sourcePaths = GraphAlgorithms.Dijkstra(graph, move.Destination);

                // From our possible source to evacuation vertex:
                var sourceEvacuationPaths = evacuationPaths.Where(p => p.Source.Equals(move.Destination)).ToList();
                var sourceToEvacuation = sourceEvacuationPaths.FirstOrDefault(p =>
                    Math.Abs(p.TraverseWeight(traversePassengers, settings.SlowDown).Item1 -
                             sourceEvacuationPaths.Min(p2 =>
                                 p2.TraverseWeight(traversePassengers, settings.SlowDown).Item1)) < 0.01);

                // If can't reach
                if (sourceToEvacuation == null || sourceToEvacuation.Weight == int.MaxValue)
                {
                    unreachable.Add(evacuationVertex);
                    continue;
                }

                reachedAny = true;
                // From evacuation vertex to shelter:
                var evacuationToShelter = ShortestPathToShelter(evacuationPaths).Reverse();

                var (wSourceToEvacuation, pSourceToEvacuation) = sourceToEvacuation.TraverseWeight(traversePassengers, settings.SlowDown);
                var (wEvacuationToShelter, _) = evacuationToShelter.TraverseWeight(sourceToEvacuation.GetVertices(), pSourceToEvacuation, agent.Settings.SlowDown);
                var wSourceEvacuationShelter = wSourceToEvacuation + wEvacuationToShelter;

                // From source to shelter to evacuation to shelter:
                var sourceToShelter = ShortestPathToShelter(sourcePaths);
                var shelterEvacuationPaths =
                    evacuationPaths.Where(p => p.Source.Equals(sourceToShelter.Source)).ToList();
                var shelterToEvacuation = shelterEvacuationPaths.FirstOrDefault(p =>
                                          Math.Abs(p.TraverseWeight(traversePassengers, settings.SlowDown).Item1 -
                                                   shelterEvacuationPaths.Min(p2 =>
                                                       p2.TraverseWeight(traversePassengers, settings.SlowDown).Item1)) < 0.01);
                sourceToShelter = sourceToShelter.Reverse();

                var wSourceToShelterToEvacuationToShelter = double.MaxValue;
                if (shelterToEvacuation != null)
                {
                    var (wSourceToShelter, _) = sourceToShelter.TraverseWeight(traversePassengers,
                        settings.SlowDown);
                    var (wShelterToEvacuation, pShelterToEvacuation) =
                        shelterToEvacuation.TraverseWeight(sourceToShelter.GetVertices(), 0, settings.SlowDown);
                    var (wLongerEvacuationToShelter, _) = evacuationToShelter.TraverseWeight(
                        shelterToEvacuation.GetVertices().Union(sourceToShelter.GetVertices()).ToList(), pShelterToEvacuation,
                        settings.SlowDown);

                    wSourceToShelterToEvacuationToShelter =
                        wSourceToShelter + wShelterToEvacuation + wLongerEvacuationToShelter;
                }
                

                if (totalTime + Math.Min(wSourceEvacuationShelter, wSourceToShelterToEvacuationToShelter) >= deadline)
                {
                    unreachable.Add(evacuationVertex);
                }
            }

            if (!reachedAny && agent.Passengers > 0)
            {
                var destinationPaths = GraphAlgorithms.Dijkstra(graph, move.Destination);
                var positionToShelter = ShortestPathToShelter(destinationPaths).Reverse();
                var (weight, _) = positionToShelter.TraverseWeight(agent.Passengers, settings.SlowDown);
                if (totalTime + weight >= deadline)
                {
                    unreachable.Add(new EvacuationVertex(-1, agent.Passengers));
                }
            }

            var unreachablePeople = unreachable.Aggregate(0, (sum, agg) => sum + agg.PeopleCount);
            var passengers = agent.Passengers;
            if (move.Destination is EvacuationVertex ev)
            {
                passengers += ev.PeopleCount;
            }

            return new HeuristicResult(move, unreachablePeople * (Math.Ceiling(graph.Edges.Max(e => e.Weight)/100d) * 100), passengers, totalTime, deadline);
        }

        private IPath ShortestPathToShelter(IList<IPath> paths)
        {
            var pathsToShelter = paths.Where(p => p.Source is ShelterVertex).ToList();

            return pathsToShelter.Aggregate(pathsToShelter[0], (minWeight, newWeight) =>
                    newWeight.Weight < minWeight.Weight ? newWeight : minWeight);
        }
    }
}
