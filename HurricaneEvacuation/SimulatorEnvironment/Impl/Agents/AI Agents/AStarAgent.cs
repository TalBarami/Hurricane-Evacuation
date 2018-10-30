using System;
using HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.AI_Agents
{
    internal class AStarAgent : AbstractAiAgent
    {
        public AStarAgent(int id, ISettings settings, IVertex position) : base(id, settings, position)
        {
            HeuristicFunction = new AStarFunction();
        }

        protected override IAction PlayNext(double time)
        {
            throw new NotImplementedException();
        }
    }
}
