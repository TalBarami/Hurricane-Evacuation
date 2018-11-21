using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Actions
{
    internal class NoOp : AbstractAction
    {
        public override double Cost => 1;

        public NoOp(IState oldState, IState newState, int performer) : base(oldState, newState, performer)
        {
            UpdateTime();
        }

        public override string ToString()
        {
            return $"{Performer.Name} decided to do nothing at cost {Cost}.";
        }
    }
}
