using System;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    internal class GreedyHeuristicAgent : AbstractAiAgent
    {
        public GreedyHeuristicAgent(int id, IVertex position, IHeuristicFunction heuristicFunction) : base(id, position, heuristicFunction)
        {
        }

        public override IAction PlayNext()
        {
            throw new NotImplementedException();
        }
    }
}
