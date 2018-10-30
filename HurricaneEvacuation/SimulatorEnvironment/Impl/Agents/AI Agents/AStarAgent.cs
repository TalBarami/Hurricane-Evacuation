using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.AI_Agents
{
    internal class AStarAgent : AbstractAiAgent
    {
        private double travelTime;
        public AStarAgent(int id, ISettings settings, IVertex position) : base(id, settings, position)
        {
            travelTime = 0;
            HeuristicFunction = new UnreachablePeopleFunction();
        }

        protected override IAction PlayNext(double time)
        {
            var actions = FindMinimalHValues(Position, time);
            var selectedAction = actions.Keys.First();
            travelTime += selectedAction.Cost;
            return selectedAction;
        }

        private Dictionary<IAction, double> FindMinimalHValues(IVertex source, double time)
        {
            var minimalHValues = GetHValues(source, time);
            return minimalHValues.Where(move => Math.Abs(move.Value - minimalHValues.Min(m => m.Value)) < Tolerance)
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        private Dictionary<IAction, double> GetHValues(IVertex source, double time)
        {
            return source.ValidEdges().Select(e =>
            {
                var possibleTraverse = Traverse(e);
                var hValue = HeuristicFunction.Value(Settings, possibleTraverse.Destination, time + possibleTraverse.Cost);
                return (possibleTraverse, hValue + travelTime + possibleTraverse.Cost);
            }).ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);
        }
    }
}
