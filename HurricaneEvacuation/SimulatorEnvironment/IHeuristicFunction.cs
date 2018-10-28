using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.SimulatorEnvironment
{
    internal interface IHeuristicFunction
    {
        void Visit(EmptyVertex v);
        void Visit(EvacuationVertex v);
        void Visit(ShelterVertex v);
    }
}
