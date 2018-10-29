namespace HurricaneEvacuation.SimulatorEnvironment
{
    internal interface IAction
    {
        IVertex Destination { get; }
        double Cost { get; }
        void Approve();
    }
}
