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
        protected AbstractAiAgent(int id, ISettings settings, IVertex position) : base(id, settings, position)
        {
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
                bestPicks = results.Where(hr => hr.Passengers > 0).ToList();
                if (!bestPicks.Any())
                {
                    bestPicks = results;
                }
            }
            return bestPicks.First(hr => hr.Passengers == bestPicks.Max(r => r.Passengers)).Action;
        }
    }
}
