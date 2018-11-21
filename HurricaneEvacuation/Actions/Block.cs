using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Actions
{
    internal class Block : AbstractAction
    {
        public override double Cost => 1;

        private readonly int v1;
        private readonly int v2;

        public Block(IState oldState, IState newState, int performer, int destination) : base(oldState, newState, performer)
        {
            v1 = Performer.Position;
            v2 = destination;

            NewState.Graph.RemoveEdge(v1, v2);
            UpdateTime();
        }

        public override string ToString()
        {
            return $"{Performer.Name} decided to block ({OldState.Graph.Vertex(v1)}, {OldState.Graph.Vertex(v2)}) at cost {Cost}";
        }
    }
}
