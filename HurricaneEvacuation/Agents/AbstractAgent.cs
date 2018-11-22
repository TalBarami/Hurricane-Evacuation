using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Environment;
using HurricaneEvacuation.GraphComponents.Vertices;

namespace HurricaneEvacuation.Agents
{
    internal abstract class AbstractAgent : IAgent
    {
        public int Id { get; }
        public int Position { get; set; }
        public abstract double Score { get; }

        protected AbstractAgent(int id, int position)
        {
            Id = id;
            Position = position;
        }

        public IAction NextStep(IState oldState)
        {
            return CalculateMove(oldState);
        }

        public List<IAction> PossibleMoves(IState state)
        {
            var neighbors = state.Graph.Vertex(Position).Neighbors;

            var possibleMoves = new List<IAction> { new NoOp(state, state.Clone(), Id) };
            possibleMoves.AddRange(neighbors.Select(v => new Traverse(state, state.Clone(), Id, v))
                .Where(act => act.NewState.Time <= Constants.Deadline)
                .ToList());
            return possibleMoves;
        }

        public string Name => $"{GetType().Name}{Id}";
        public abstract double CalculateWeight(int edgeWeight);
        public abstract string Visit(IVertex destination);
        public abstract IAgent Clone();

        public override string ToString()
        {
            return $"{Name}(Position {Position})";
        }

        protected abstract IAction CalculateMove(IState oldState);
    }
}
