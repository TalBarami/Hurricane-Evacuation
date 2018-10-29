namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    internal abstract class AbstractAiAgent : VehicleAgent
    {
        protected IHeuristicFunction HeuristicFunction;

        protected AbstractAiAgent(int id, IVertex position, IHeuristicFunction heuristicFunction) : base(id, position)
        {
            HeuristicFunction = heuristicFunction;
        }
    }
}
