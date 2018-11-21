using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Agents.AI_Agents
{
    internal class HeuristicResult
    {
        public IAction Action { get; }
        public IState SimulatedState { get; private set; }
        public double Value { get; }

        public HeuristicResult(IAction action, double value)
        {
            Action = action;
            Value = value;
            SimulateOtherAgents(action.Performer.Id);
        }

        public override string ToString()
        {
            return $"{Value}->{Action}";
        }

        public void SimulateOtherAgents(int performerId)
        {
            SimulatedState = Action.NewState;
            while (SimulatedState.CurrentAgent != performerId)
            {
                var newAction = SimulatedState.Agents[SimulatedState.CurrentAgent].NextStep(SimulatedState);
                SimulatedState = newAction.NewState;
            }
        }
    }
}
