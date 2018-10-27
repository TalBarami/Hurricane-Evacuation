namespace HurricaneEvacuation.SimulatorEnvironment
{
    internal interface IAction
    {
        IVertex Destination { get; set; }
        double Cost { get; }
    }
}
