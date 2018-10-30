using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.AI_Agents
{
    internal class GreedySearchAgent : AbstractAiAgent
    {
        public GreedySearchAgent(int id, ISettings settings, IVertex position) : base(id, settings, position)
        {
            HeuristicFunction = new UnreachablePeopleFunction();
        }

        protected override IAction PlayNext(double time)
        {
            /*var actions = GraphAlgorithms.Hey(Settings.Graph, Position, time, Settings.Deadline, HeuristicFunction)
                .Where(action => action.Source != Position).ToList();
            return Traverse(Position.ValidEdges().First(e => e.OtherV(Position) == actions[0].Source));*/

            var actions = FindMinimalHValues(Position, time);
            return actions.Keys.First();
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
                return (possibleTraverse, hValue);
            }).ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);
        }
    }
}
