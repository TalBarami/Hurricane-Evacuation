using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Agents.Search_Agents;
using HurricaneEvacuation.Environment;
using HurricaneEvacuation.GraphComponents;
using HurricaneEvacuation.GraphComponents.Vertices;

namespace HurricaneEvacuation.Agents.Multi_Agents
{
    abstract class AbstractMultiAgent : AbstractHeuristicAgent
    {
        protected AbstractMultiAgent(int id, int position) : base(id, position)
        {
        }

        protected AbstractMultiAgent(int id, int position, int passengers, int peopleSaved) : base(id, position, passengers, peopleSaved)
        {
        }

        public abstract double MultiScore(IState state);

        public abstract double SemiHeuristic(IState state);

        public double Reachable(IState state)
        {
            var graph = state.Graph;

            var source = graph.Vertex(Position);
            var evacuationVertices = state.EvacuationVertices;
            var shelterVertices = state.ShelterVertices;

            
            var sourceShelter = new List<Path>();
            shelterVertices.ForEach(shelter => sourceShelter.AddRange(graph.Dfs(source, shelter)));
            var simulatedSourceShelter = sourceShelter.Select(p => p.SimulateTraverse(state, Id)).ToList();
            if (simulatedSourceShelter.Any())
            {
                var finalState = MinimalState(simulatedSourceShelter);
                if (finalState.Time > Constants.Deadline)
                {
                    return 0;
                }
            }

            var reachable = new List<EvacuationVertex> {new EvacuationVertex(-1, Passengers)};

            foreach (var evacuationVertex in evacuationVertices)
            {
                var sourceEvacuationPaths = graph.Dfs(source, evacuationVertex);

                var simulatedSourceEvacuation = sourceEvacuationPaths.Select(p => p.SimulateTraverse(state, Id)).ToList();
                if (!simulatedSourceEvacuation.Any())
                {
                    continue;
                }
                var sourceToEvacuation = MinimalState(simulatedSourceEvacuation);

                var evacuationShelterPaths = new List<Path>();
                shelterVertices.ForEach(shelter => evacuationShelterPaths.AddRange(graph.Dfs(evacuationVertex, shelter)));

                var simulatedEvacuationShelter = evacuationShelterPaths.Select(p => p.SimulateTraverse(sourceToEvacuation, Id)).ToList();
                if (!simulatedEvacuationShelter.Any())
                {
                    continue;
                }
                var evacuationToShelter = MinimalState(simulatedEvacuationShelter);

                if (evacuationToShelter.Time <= Constants.Deadline)
                {
                    reachable.Add(evacuationVertex);
                }
            }

            var reachablePeople = reachable.Aggregate(0, (sum, agg) => sum + agg.PeopleCount);
            return reachablePeople;
        }
        /*public double Reachable(IState state)
        {
            var unreachable = Unreachable(state);

            var people = state.EvacuationVertices.Aggregate(0, (sum, ev) => sum + ev.PeopleCount) +
                         state.HelpfulAgents.Aggregate(0, (sum, agent) => sum + agent.Passengers);

            return people - unreachable;
        }

        public double Unreachable(IState state)
        {
            var graph = state.Graph;
            var competitors = state.HelpfulAgents;

            var source = graph.Vertex(Position);
            var evacuationVertices = state.EvacuationVertices;
            var shelterVertices = state.ShelterVertices;

            var unreachable = new List<EvacuationVertex>
            {
                new EvacuationVertex(-2, competitors.Where(c => c.Id != Id).Aggregate(0, (sum, next) => sum + next.Passengers))
            };


            if (Passengers > 0)
            {
                var sourceShelter = new List<Path>();
                shelterVertices.ForEach(shelter => sourceShelter.AddRange(graph.Dfs(source, shelter)));
                var simulatedSourceShelter = sourceShelter.Select(p => p.SimulateTraverse(state, Id)).ToList();
                if (simulatedSourceShelter.Any())
                {
                    var finalState = MinimalState(simulatedSourceShelter);

                    if (finalState.Time > Constants.Deadline)
                    {
                        unreachable.Add(new EvacuationVertex(-1, Passengers));
                    }
                }
                else
                {
                    unreachable.Add(new EvacuationVertex(-1, Passengers));
                }
            }

            foreach (var evacuationVertex in evacuationVertices)
            {
                var sourceEvacuationPaths = graph.Dfs(source, evacuationVertex);

                var simulatedSourceEvacuation = sourceEvacuationPaths.Select(p => p.SimulateTraverse(state, Id)).ToList();
                if (!simulatedSourceEvacuation.Any())
                {
                    unreachable.Add(evacuationVertex);
                    continue;
                }
                var sourceToEvacuation = MinimalState(simulatedSourceEvacuation);

                var evacuationShelterPaths = new List<Path>();
                shelterVertices.ForEach(shelter => evacuationShelterPaths.AddRange(graph.Dfs(evacuationVertex, shelter)));

                var simulatedEvacuationShelter = evacuationShelterPaths.Select(p => p.SimulateTraverse(sourceToEvacuation, Id)).ToList();
                if (!simulatedEvacuationShelter.Any())
                {
                    unreachable.Add(evacuationVertex);
                    continue;
                }
                var evacuationToShelter = MinimalState(simulatedEvacuationShelter);

                if (evacuationToShelter.Time > Constants.Deadline)
                {
                    unreachable.Add(evacuationVertex);
                }
            }

            var unreachablePeople = unreachable.Aggregate(0, (sum, agg) => sum + agg.PeopleCount);
            return unreachablePeople;
        }*/

        public IState MinimalState(List<IState> states)
        {
            return states.Aggregate(states[0], (min, current) => current.Time < min.Time ? current : min);
        }
    }
}
