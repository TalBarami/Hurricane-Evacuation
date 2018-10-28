namespace HurricaneEvacuation.SimulatorEnvironment
{
    internal interface IPath
    {
        IVertex Source { get; }
        IPath Next { get; }
        int Weight { get; }

    }
}
