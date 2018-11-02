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
            var hValues = GetHValues(Position, time);
            var minimal = hValues.Where(hResult => Math.Abs(hResult.HValue - hValues.Min(h => h.HValue)) < Tolerance).ToList();
            //Console.WriteLine($"Function values: {hValues.Aggregate("", (cur, agg) => $"{cur} {agg}")}");
            Console.WriteLine($"H Returned: {hValues.Aggregate("", (cur, agg) => $"{cur} {agg.HValueToString()}")}");
            return PickBest(minimal);
        }
    }
}
