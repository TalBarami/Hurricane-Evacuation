using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    internal class HumanAgent : AbstractAgent
    {
        public HumanAgent(int id, IVertex position) : base(position)
        {
            Id = $"HumanAgent{id}";
        }

        public override IAction PlayNext(IGraph world)
        {
            Console.WriteLine($"{Id}, your current position is: {Position}");
            Console.WriteLine($"World State is:\n{world}");
            var validEdges = Position.ValidEdges();
            var validNeighbors = validEdges.Select(e => e.OtherV(Position)).ToList();
            Console.WriteLine($"You can stay at {Position.Id} or move to: {validNeighbors.ListToString()}");
            int dst;
            while (!int.TryParse(Console.ReadLine(), out dst) || (dst != Position.Id && validNeighbors.All(v => v.Id != dst)))
            {
                Console.WriteLine("Try again...");
            }

            var destinationVertex = validNeighbors.First(v => v.Id == dst);
            if (dst == Position.Id)
            {
                return new NoOperation(Position);
            }

            return new Traverse(destinationVertex, this, validEdges.First(e => e.Contains(Position, destinationVertex)));
        }
    }
}
