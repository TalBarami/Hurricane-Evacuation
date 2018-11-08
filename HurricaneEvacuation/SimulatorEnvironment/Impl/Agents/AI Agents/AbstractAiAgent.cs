using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.NormalAgents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.AI_Agents
{
    internal abstract class AbstractAiAgent : VehicleAgent
    {
        protected double Tolerance = 0.0001;
        protected IHeuristicFunction HeuristicFunction { get; set; }
        protected double Performance => Settings.WeightConstant * PeopleSaved + HeuristicFunction.SearchExpansions;
        public override double Score => Performance;

        private Dictionary<IVertex, int> visited;
        protected AbstractAiAgent(int id, ISettings settings, IVertex position) : base(id, settings, position)
        {
            visited = settings.Graph.Vertices.ToDictionary((k => k), (k => 0));
            visited[Position]++;
        }

        protected IList<HeuristicResult> GetHValues(IVertex source, double time)
        {
            return source.ValidEdges().Select(e =>
            {
                var possibleTraverse = Traverse(e);
                return HeuristicFunction.Apply(this, possibleTraverse as Traverse, time);
            }).ToList();
        }
        protected IAction PickBest(IList<HeuristicResult> results)
        {
            IList<HeuristicResult> bestPicks = results.Where(hr => hr.Passengers > 0 && hr.Shelter).ToList();
            if (!bestPicks.Any())
            {
                /*bestPicks = results.Where(hr => hr.Passengers > 0).ToList();
                if (!bestPicks.Any())
                {*/
                    bestPicks = results.Where(hr =>
                        visited[hr.Action.Destination] == results.Min(h => visited[h.Action.Destination])).ToList();
                    if (!bestPicks.Any())
                    {
                        bestPicks = results;
                    }
                /*}*/
            }
            var nextMove = bestPicks.First(hr => hr.Passengers == bestPicks.Max(r => r.Passengers)).Action;
            visited[nextMove.Destination]++;
            return nextMove;
        }
    }
}
