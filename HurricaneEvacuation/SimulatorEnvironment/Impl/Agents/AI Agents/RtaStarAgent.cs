using System;
using HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.AI_Agents
{
    internal class RtaStarAgent : AbstractAiAgent
    {
        public RtaStarAgent(int id, ISettings settings, IVertex position) : base(id, settings, position)
        {
            HeuristicFunction = new UnreachablePeopleFunction();
        }

        protected override IAction PlayNext(double time)
        {
            throw new NotImplementedException();
        }
    }
}
