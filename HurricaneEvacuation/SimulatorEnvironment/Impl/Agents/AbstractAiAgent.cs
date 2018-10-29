namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    internal abstract class AbstractAiAgent : VehicleAgent
    {
        protected IHeuristicFunction HeuristicFunction { get; }
        protected double Score { get; set; }
        protected int SearchExpansion { get; set; }

        protected AbstractAiAgent(int id, IVertex position, IHeuristicFunction heuristicFunction) : base(id, position)
        {
            HeuristicFunction = heuristicFunction;
        }
    }
}
