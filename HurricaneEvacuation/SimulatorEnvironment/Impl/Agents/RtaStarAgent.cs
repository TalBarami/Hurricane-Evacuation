using System;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    internal class RtaStarAgent : AbstractAiAgent
    {
        public RtaStarAgent(int id, IVertex position, IHeuristicFunction heuristicFunction) : base(id, position, heuristicFunction)
        {
        }

        protected override IAction PlayNext()
        {
            throw new NotImplementedException();
        }
    }
}
