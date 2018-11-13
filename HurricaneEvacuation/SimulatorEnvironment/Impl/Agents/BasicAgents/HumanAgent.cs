using System;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.BasicAgents
{
    internal class HumanAgent : VehicleAgent
    {
        public HumanAgent(int id, ISettings settings, IVertex position) : base(id, settings, position)
        {
        }

        protected override IAction PlayNext(double time)
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
            return dst == Position.Id ? NoOperation() : Traverse(validEdges.First(e => e.Contains(Position, destinationVertex)));
        }
    }
}
