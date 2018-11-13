using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions
{
    class UnreachablePeopleFunction : AbstractHeuristicFunction
    { 
        public override HeuristicResult Apply(IState state)
        {
            SearchExpansions++;
            var settings = state.Settings;

            var graph = settings.Graph;
            var deadline = settings.Deadline;
            var totalTime = state.Action.Cost + state.Time;
            var dst = state.Action.Destination;

            var evacuationVertices = graph.Vertices.OfType<EvacuationVertex>().Where(v => !state.Visited.Contains(v) && v.PeopleCount > 0).ToList();
            var unreachable = new List<EvacuationVertex>();
            
            foreach (var evacuationVertex in evacuationVertices)
            {
                var traversePassengers = state.Passengers;
                /*if (evacuationVertex == dst)
                {
                    traversePassengers += evacuationVertex.PeopleCount;
                }*/

                var newState = new State(state, traversePassengers, totalTime);
                var evacuationPaths = GraphAlgorithms.Dijkstra(evacuationVertex, newState).ToList();
                var sourcePaths = GraphAlgorithms.Dijkstra(dst, newState).ToList();

                // From our possible source to evacuation vertex:
                var sourceEvacuationPaths = evacuationPaths.Where(p => p.Source.Equals(dst)).ToList();
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
                
                // From evacuation vertex to shelter:
                var evacuationToShelter = ShortestPathToShelter(evacuationPaths).Reverse();

                var (wSourceToEvacuation, pSourceToEvacuation) = sourceToEvacuation.TraverseWeight(traversePassengers, settings.SlowDown);
                var (wEvacuationToShelter, _) = evacuationToShelter.TraverseWeight(sourceToEvacuation.GetVertices(), pSourceToEvacuation, settings.SlowDown);
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
                

                if (totalTime + Math.Min(wSourceEvacuationShelter, wSourceToShelterToEvacuationToShelter) > deadline)
                {
                    unreachable.Add(evacuationVertex);
                }
            }

            if (state.Passengers > 0) // Verify that the agent got enough time to return to the shelter
            {
                var destinationPaths = GraphAlgorithms.Dijkstra(dst, new State(state, state.Passengers, totalTime)).ToList();
                var positionToShelter = ShortestPathToShelter(destinationPaths).Reverse();

                var (weight, _) = positionToShelter.TraverseWeight(state.Passengers, settings.SlowDown);
                if ((positionToShelter.Weight == 0 && positionToShelter.Source.Id != dst.Id) || totalTime + weight > deadline)
                {
                    unreachable.Add(new EvacuationVertex(-1, state.Passengers));
                }
            }

            var unreachablePeople = unreachable.Aggregate(0, (sum, agg) => sum + agg.PeopleCount);
            var passengers = state.Passengers;
            switch (dst)
            {
                case EvacuationVertex ev:
                    passengers += ev.PeopleCount;
                    break;
                case ShelterVertex _:
                    passengers = 0;
                    break;
            }

            return new HeuristicResult(state.Action, unreachablePeople * (Math.Ceiling(graph.Edges.Max(e => e.Weight)/100d) * 100), passengers, totalTime, deadline);
        }

        private IPath ShortestPathToShelter(IList<IPath> paths)
        {
            var pathsToShelter = paths.Where(p => p.Source is ShelterVertex).ToList();

            return pathsToShelter.Aggregate(pathsToShelter[0], (minWeight, newWeight) =>
                    newWeight.Weight < minWeight.Weight ? newWeight : minWeight);
        }
    }
}
