namespace HurricaneEvacuation.SimulatorEnvironment
{
    interface IAction
    {
        int Cost(IAgent a, IEdge e);
    }
}
