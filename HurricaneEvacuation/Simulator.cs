using System;
using System.Linq;
using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation
{
    public class Simulator
    {
        private IState world;

        public Simulator(IState initialState)
        {
            world = initialState;
        }

        public void Start()
        {
            while (world.Time < Constants.Deadline)
            {
                Console.WriteLine(world);

                var action = world.Agents[world.CurrentAgent].NextStep(world);
                Console.WriteLine(action);
                if (action.NewState.Time > Constants.Deadline)
                {
                    Console.WriteLine($"There was not enough time for {action.Performer.Name} to complete his action.");
                    break;
                }

                world = action.NewState.Clone();

                Console.WriteLine();
                Console.ReadKey(true);
            }
            Console.WriteLine(world);
            Console.WriteLine("--- All your base are belong to us. ---");
            Console.WriteLine($"Final score:\n\t\t{string.Join("\n\t\t", world.Agents.Select(a => $"{a.Name}: {a.Score}"))}");
            Console.ReadKey(true);
        }

    }
}
