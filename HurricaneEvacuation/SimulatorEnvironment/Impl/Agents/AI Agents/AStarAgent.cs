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
            var hValues = GetHValues(Position, time);
            var minimal = hValues.Where(hResult => Math.Abs(hResult.FValue(travelTime) - hValues.Min(h => h.FValue(travelTime))) < Tolerance).ToList();
            //Console.WriteLine($"Real values: {hValues.Aggregate("", (cur, agg) => $"{cur} {agg}")}");
            Console.WriteLine($"H Returned: {hValues.Aggregate("", (cur, agg) => $"{cur} {agg.HValueToString()}")}");
            Console.WriteLine($"F Returned: {hValues.Aggregate("", (cur, agg) => $"{cur} {agg.FValueToString(travelTime)}")}");
            var selected = PickBest(minimal);
            travelTime += selected.Cost;
            return selected;
        }
    }
}
