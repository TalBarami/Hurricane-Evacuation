using HurricaneEvacuation.Actions;

namespace HurricaneEvacuation.Agents
{
    internal class HeuristicResult
    {
        public IAction Action { get; }
        public double Value { get; set; }

        public HeuristicResult(IAction action, double value)
        {
            Action = action;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value}->{Action}";
        }
    }
}
