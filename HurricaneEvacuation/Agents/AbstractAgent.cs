using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Environment;
using HurricaneEvacuation.GraphComponents.Vertices;

namespace HurricaneEvacuation.Agents
{
    public abstract class AbstractAgent : IAgent
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
