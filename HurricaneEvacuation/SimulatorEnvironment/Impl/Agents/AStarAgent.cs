using System;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    internal class AStarAgent : AbstractAiAgent
    {
        public AStarAgent(int id, IVertex position, IHeuristicFunction heuristicFunction) : base(id, position, heuristicFunction)
        {
        }

        public override IAction PlayNext()
        {
            throw new NotImplementedException();
        }
    }
}
