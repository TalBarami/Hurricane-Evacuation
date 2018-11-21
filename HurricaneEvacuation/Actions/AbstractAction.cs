using HurricaneEvacuation.Agents;
using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Actions
{
    internal abstract class AbstractAction : IAction
    {
        protected AbstractAction(IState oldState, IState newState, int performer)
        {
            OldState = oldState;
            NewState = newState;
            this.performer = performer;
        }

        protected void UpdateTime()
        {
            NewState.Time += Cost;
            NewState.CurrentAgent = (NewState.CurrentAgent + 1) % NewState.Agents.Count;
        }

        public abstract double Cost { get; }
        public IState OldState { get; }
        public IState NewState { get; set; }
        public IAgent Performer => NewState.Agents[performer];
        private readonly int performer;
    }
}
