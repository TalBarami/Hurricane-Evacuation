using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Agents.AI_Agents;
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

        protected override IAction CalculateMove(IState state)
        {
            var tree = new MinimaxTree(new Traverse(state, Id));
            return tree.SelectedAction;
        }

        public override HeuristicResult Heuristic(IAction action)
        {
            return null; /*new HeuristicResult(action, Reachable(action));*/
        }

        public double Reachable(IAction action)
        {
            var unreachable = Unreachable(action);

            var people = action.NewState.EvacuationVertices.Aggregate(0, (sum, ev) => ev.PeopleCount) +
                         action.NewState.HelpfulAgents.Aggregate(0, (sum, agent) => agent.Passengers);
            return people - unreachable;
        }

        public double Unreachable(IAction action)
        {
            var state = action.NewState;
            var graph = state.Graph;

            var source = graph.Vertex(action.Performer.Position);
            var evacuationVertices = state.EvacuationVertices;
            var shelterVertices = state.ShelterVertices;

            var unreachable = new List<EvacuationVertex>();

            if (action.Performer is AbstractAIAgent performer && performer.Passengers > 0)
            {
                var sourceShelter = new List<Path>();
                shelterVertices.ForEach(shelter => sourceShelter.AddRange(graph.Dfs(source, shelter)));
                var simulatedSourceShelter = sourceShelter.Select(p => p.SimulateTraverse(state, Id)).ToList();
                if (simulatedSourceShelter.Any())
                {
                    var finalState = MinimalState(simulatedSourceShelter);

                    if (finalState.Time > Constants.Deadline)
                    {
                        /*if (graph.Vertex(performer.Position) is ShelterVertex)
                        {
                            if (action.OldState.Agents[Id] is AIAgent oldAgent)
                            {
                                unreachable.Add(new EvacuationVertex(-1, oldAgent.Passengers));
                            }
                        }
                        else
                        {*/
                        unreachable.Add(new EvacuationVertex(-1, performer.Passengers));
                        /*}*/
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
                /*var sourceToEvacuation = Path.ShortestTraversePath(sourceEvacuationPaths, state, Id);
                if (sourceToEvacuation.Vertices.Count <= 0)
                {
                    unreachable.Add(evacuationVertex);
                    continue;
                }*/

                var evacuationShelterPaths = new List<Path>();
                shelterVertices.ForEach(shelter => evacuationShelterPaths.AddRange(graph.Dfs(evacuationVertex, shelter)));

                var simulatedEvacuationShelter = evacuationShelterPaths.Select(p => p.SimulateTraverse(sourceToEvacuation, Id)).ToList();
                if (!simulatedEvacuationShelter.Any())
                {
                    unreachable.Add(evacuationVertex);
                    continue;
                }
                var evacuationToShelter = MinimalState(simulatedEvacuationShelter);
                /*var evacuationToShelter = Path.ShortestTraversePath(evacuationShelterPaths,
                    sourceToEvacuation.SimulateTraverse(state, Id), Id);*/

                /*var sourceToEvacuationToShelter = sourceToEvacuation.Append(evacuationToShelter);
                var finalState = sourceToEvacuationToShelter.SimulateTraverse(state, Id);
*/

                if (evacuationToShelter.Time > Constants.Deadline)
                {
                    unreachable.Add(evacuationVertex);
                }
            }

            var unreachablePeople = unreachable.Aggregate(0, (sum, agg) => sum + agg.PeopleCount);
            return unreachablePeople;
        }

        public IState MinimalState(List<IState> states)
        {
            return states.Aggregate(states[0], (min, current) => current.Time < min.Time ? current : min);
        }
    }
}
