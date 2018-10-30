using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.NormalAgents;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.AI_Agents
{
    internal abstract class AbstractAiAgent : VehicleAgent
    {
        protected IHeuristicFunction HeuristicFunction { get; set; }
        protected double Score { get; set; }
        protected int SearchExpansion { get; set; }

        protected AbstractAiAgent(int id, ISettings settings, IVertex position) : base(id, settings, position)
        {
        }
    }
}
