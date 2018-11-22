using HurricaneEvacuation.Agents;
using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Actions
{
    internal interface IAction
    {
        double Cost { get; }
        IState OldState { get; }
        IState NewState { get; }
        IAgent Performer { get; }
    }
}
