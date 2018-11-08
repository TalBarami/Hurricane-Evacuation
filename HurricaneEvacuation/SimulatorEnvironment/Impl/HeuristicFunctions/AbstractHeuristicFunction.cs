using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions
{
    abstract class AbstractHeuristicFunction : IHeuristicFunction
    {

        protected AbstractHeuristicFunction()
        {
            SearchExpansions = 0;
        }

        public int SearchExpansions { get; protected set; }
        public abstract HeuristicResult Apply(IAgent agent, Traverse move, double time);
    }
}
