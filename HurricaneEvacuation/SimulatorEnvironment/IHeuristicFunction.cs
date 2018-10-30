using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.SimulatorEnvironment
{
    internal interface IHeuristicFunction
    {
        double Value(IGraph graph, IVertex source, double time, double deadline);
    }
}
