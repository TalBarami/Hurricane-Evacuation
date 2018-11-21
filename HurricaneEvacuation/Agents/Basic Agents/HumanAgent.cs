using System;
using System.Linq;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Agents.Basic_Agents
{
    internal class HumanAgent : AbstractHelpfulAgent
    {
        public HumanAgent(int id, int position) : base(id, position)
        {
        }

        public HumanAgent(HumanAgent other) : base(other.Id, other.Position, other.Passengers, other.PeopleSaved)
        {
        }

        public override IAgent Clone()
        {
            return new HumanAgent(this);
        }

        protected override IAction CalculateMove(IState oldState)
        {
            var position = oldState.Graph.Vertex(Position);
            var newAgent = new HumanAgent(this);
            var newState = oldState.Clone();
            newState.UpdateAgent(newAgent);

            Console.WriteLine($"{Name}, your current position is: {position}");
            var validNeighbors = position.Neighbors.Select(v => oldState.Graph.Vertex(v)).ToList();
            Console.WriteLine($"You can stay at {position} or move to: {string.Join(", ", validNeighbors)}");
            int dst;
            while (!int.TryParse(Console.ReadLine(), out dst) || (dst != position.Id && validNeighbors.All(v => v.Id != dst)))
            {
                Console.WriteLine("Try again...");
            }

            if (dst == position.Id)
            {
                return new NoOp(oldState, newState, Id);
            }

            return new Traverse(oldState, newState, Id, dst);
        }
    }
}
