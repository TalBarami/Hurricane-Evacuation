using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Environment;
using HurricaneEvacuation.GraphComponents;
using HurricaneEvacuation.GraphComponents.Vertices;

namespace HurricaneEvacuation.Agents.Search_Agents
{
    internal abstract class AbstractSearchAgent : AbstractHeuristicAgent
    {
        protected int SearchExpansions { get; set; }
        protected double Performance => Constants.WeightConstant * PeopleSaved + SearchExpansions;
        public override double Score => Performance;

        protected AbstractSearchAgent(int id, int position) : base(id, position)
        {
        }

        protected AbstractSearchAgent(int id, int position, int passengers, int peopleSaved, int searchExpansions) : base(id, position, passengers, peopleSaved)
        {
            SearchExpansions = searchExpansions;
        }
        
        public abstract ExpandTree CreateTree(IState state);

        protected override IAction CalculateMove(IState oldState)
        {
            var tree = CreateTree(oldState);
            var action = tree.Result.Action;

            if (!(action.Performer.Clone() is AbstractSearchAgent newAgent)) throw new Exception("Expand on non-AI agent.");

            newAgent.SearchExpansions += tree.Expands;
            action.NewState.UpdateAgent(newAgent);

            return action;
        }

        public double Unreachable(IAction action)
        {
            var state = action.NewState;
            var graph = state.Graph;

            var source = graph.Vertex(action.Performer.Position);
            var evacuationVertices = state.EvacuationVertices;
            var shelterVertices = state.ShelterVertices;

            var unreachable = new List<EvacuationVertex>();

            if (action.Performer is AbstractSearchAgent performer && performer.Passengers > 0)
            {
                var sourceShelter = new List<Path>();
                shelterVertices.ForEach(shelter => sourceShelter.AddRange(graph.Dfs(source, shelter)));
                var simulatedSourceShelter = sourceShelter.Select(p => p.SimulateTraverse(state, Id)).ToList();
                if (simulatedSourceShelter.Any())
                {
                    var finalState = MinimalState(simulatedSourceShelter);

                    if (finalState.Time > Constants.Deadline)
                    {
                        unreachable.Add(new EvacuationVertex(-1, performer.Passengers));
                    }
                }
                else
                {
                    unreachable.Add(new EvacuationVertex(-1, performer.Passengers));
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
            var multiplier = Math.Ceiling(graph.Edges.Max(e => e.Weight) / 100d) * 100;
            return unreachablePeople * multiplier;
        }

        public IState MinimalState(List<IState> states)
        {
            return states.Aggregate(states[0], (min, current) => current.Time < min.Time ? current : min);
        }
        
    }
}
