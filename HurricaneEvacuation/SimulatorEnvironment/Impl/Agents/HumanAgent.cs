using System;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    internal class HumanAgent : VehicleAgent
    {
        public HumanAgent(int id, IVertex position) : base(id, position)
        {
        }

        protected override IAction PlayNext()
        {
            Console.WriteLine($"{Id}, your current position is: {Position}");
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
                return new NoOperation(this, Position);
            }

            return new Traverse(this, validEdges.First(e => e.Contains(Position, destinationVertex)));
        }
    }
}
