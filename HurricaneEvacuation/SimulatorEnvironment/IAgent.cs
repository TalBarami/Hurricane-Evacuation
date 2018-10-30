using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.SimulatorEnvironment
{
    internal interface IAgent
    {
        string Id { get; }
        IVertex Position { get; }
        int Passengers { get; }
        int PeopleSaved { get; }
        int ActionsPerformed { get; }


        double PerformStep(double time);
        void Visit(EvacuationVertex v);
        void Visit(ShelterVertex v);
        void Visit(EmptyVertex v);
        void MoveTo(IVertex destination);
    }
}
