using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Actions
{
    internal class Traverse : AbstractAction
    {
        public override double Cost { get; }

        private readonly int v1;
        private readonly int v2;
        private string visit;

        public Traverse(IState state, int performer) : base(state, state, performer)
        {
            Cost = 0;
            visit = "";
        }

        public Traverse(IState oldState, IState newState, int performer, int destination) : base(oldState, newState, performer)
        {
            v1 = Performer.Position;
            v2 = destination;
            visit = "";

            var edgeWeight = Performer.CalculateWeight(oldState.Graph.EdgeWeight(v1, v2));
            if (edgeWeight > 0)
            {
                Cost = edgeWeight;
                Performer.Position = destination;
                visit = Performer.Visit(NewState.Graph.Vertex(destination));
            }
            else
            {
                Cost = 1;
            }

            UpdateTime();
        }

        public override string ToString()
        {
            var s = $"{Performer.Name} decided to move from {OldState.Graph.Vertex(v1)} to {OldState.Graph.Vertex(v2)} at cost {Cost}";
            return visit != "" ? $"{s} and {visit}" : s;
        }
    }
}
