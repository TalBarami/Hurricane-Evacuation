using System;
using System.Collections.Generic;
using System.Threading;
using HurricaneEvacuation.SimulatorEnvironment;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Settings;

namespace HurricaneEvacuation
{
    internal class Simulator
    {
        private readonly ISettings Settings;
        private double Time { get; set; }

        public Simulator(ISettings settings)
        {
            Settings = settings;
            Time = 0;
        }

        public void Start()
        {
            var i = 0;
            while (Time < Settings.Deadline)
            {
                var currentAgent = Settings.Agents[i];
                Console.WriteLine($"Time to world's end: {Time}/{Settings.Deadline}.\nWorld state:\n{Settings.Graph}");
                Time = currentAgent.PerformStep(Time);
                i = (i + 1) % Settings.Agents.Count;
                Thread.Sleep(2000);
                Console.WriteLine();
            }

            Console.WriteLine("\nALL YOUR BASE ARE BELONG TO US.");
        }
    }
}
