using System;
using System.Threading;
using HurricaneEvacuation.SimulatorEnvironment;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation
{
    internal class Simulator
    {
        private readonly ISettings settings;
        private double Time { get; set; }

        public Simulator(ISettings settings)
        {
            this.settings = settings;
            Time = 0;
        }

        public void Start()
        {
            var i = 0;
            while (Time < settings.Deadline)
            {
                var currentAgent = settings.Agents[i];
                Console.WriteLine($"Time to world's end: {Time}/{settings.Deadline}.");
                Console.WriteLine($"World state:\n{settings.Graph}");
                Console.WriteLine($"Agents state:\n\t{settings.Agents.ListToString()}");
                Time = currentAgent.PerformStep(Time);
                i = (i + 1) % settings.Agents.Count;
                //Thread.Sleep(2000);
                Console.ReadKey(true);
                Console.WriteLine();
            }

            Console.WriteLine("\nALL YOUR BASE ARE BELONG TO US.");
        }
    }
}
