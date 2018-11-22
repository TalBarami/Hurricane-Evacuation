using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Agents.Search_Agents
{
    class SearchHeuristicResult : HeuristicResult
    {
        public IState SimulatedState { get; private set; }
        public SearchHeuristicResult(IAction action, double value) : base(action, value)
        {
            SimulateOtherAgents(action.Performer.Id);
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
