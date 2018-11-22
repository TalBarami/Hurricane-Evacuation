using System.Linq;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Environment;
using HurricaneEvacuation.GraphComponents.Vertices;

namespace HurricaneEvacuation.Agents.Basic_Agents
{
    internal class VandalAgent : AbstractAgent
    {
        public int Delay { get; }
        public int Waiting { get; set; }
        public bool Blocking { get; set; }

        public VandalAgent(int id, int position, int delay, int waiting, bool blocking) : base(id, position)
        {
            Delay = delay;
            Waiting = waiting;
            Blocking = blocking;
        }

        public VandalAgent(int id, int position, int delay) : this(id, position, delay, 0, true)
        {
        }

        public VandalAgent(VandalAgent other) : this(other.Id, other.Position, other.Delay, other.Waiting, other.Blocking)
        {
        }

        protected override IAction CalculateMove(IState oldState)
        {
            var newState = oldState.Clone();
            var newAgent = new VandalAgent(this);
            newState.UpdateAgent(newAgent);

            if (Waiting < Delay)
            {
                newAgent.Waiting = Waiting +1;
                return new NoOp(oldState, newState, Id);
            }

            newAgent.Waiting = 0;
            newAgent.Blocking = !Blocking;
            
            var neighbors = newState.Graph.Vertex(Position).Neighbors;
            if (neighbors.Count == 0)
            {
                return new NoOp(oldState, newState, Id);
            }

            var minimal = neighbors.Aggregate(neighbors[0], (min, next) =>
                newState.Graph.EdgeWeight(Position, next) < newState.Graph.EdgeWeight(Position, min) ? next : min);

            if (Blocking)
            {
                return new Block(oldState, newState, Id, minimal);
            }

            return new Traverse(oldState, newState, Id, minimal);

        }

        public override double Score => 0;

        public override double CalculateWeight(int edgeWeight)
        {
            return edgeWeight;
        }

        public override string Visit(IVertex destination)
        {
            return "";
        }

        public override IAgent Clone()
        {
            return new VandalAgent(this);
        }

        public override string ToString()
        {
            return $"{Name}(Position {Position}, Delay {Delay}, Waiting {Waiting}, Blocking {Blocking})";
        }
    }
}
