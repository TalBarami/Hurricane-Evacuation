using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions;

namespace HurricaneEvacuation.SimulatorEnvironment
{
    internal interface IHeuristicFunction
    {
        HeuristicResult Apply(IAgent agent, Traverse move, double time);
    }
}
