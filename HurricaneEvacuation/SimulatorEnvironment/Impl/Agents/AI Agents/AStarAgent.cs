using System;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

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
            foreach (var hr in hValues)
            {
                hr.TravelTime = travelTime;
            }
            var minimal = hValues.Where(hResult => Math.Abs(hResult.FValue - hValues.Min(h => h.FValue)) < Tolerance).ToList();
            Console.WriteLine($"\tF Returned:\n\t\t{hValues.ListToString().Replace(" ; ", "\n\t\t")}");
            var selected = PickBest(minimal);
            travelTime += selected.Cost;
            return selected;
        }
    }
}
