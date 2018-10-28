using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.SimulatorEnvironment
{
    internal interface IAgent
    {
        string Id { get; }
        IVertex Position { get; set; }
        int Passengers { get; set; }
        int PeopleSaved { get; set; }
        int ActionsPerformed { get; set; }


        IAction PerformStep();
        void Visit(EvacuationVertex v);
        void Visit(ShelterVertex v);
        void Visit(EmptyVertex v);
    }
}
