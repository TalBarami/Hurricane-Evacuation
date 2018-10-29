using System;
using System.Collections.Generic;
using System.Threading;
using HurricaneEvacuation.SimulatorEnvironment;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Settings;

namespace HurricaneEvacuation
{
    internal class Simulator
    {
        private IGraph World { get; set; }
        private IList<IAgent> Agents { get; set; }
        private int Deadline { get; set; }
        private double Time { get; set; }

        public Simulator()
        {
            Initialize();
        }

        private void Initialize()
        {
            var settings = SettingsSingleton.Instance;
            World = settings.Graph;
            Agents = settings.Agents;
            Deadline = settings.Deadline;
            Time = 0;
        }

        public void Start()
        {
            var i = 0;
            while (Time < Deadline)
            {
                var currentAgent = Agents[i];
                Console.WriteLine($"Time to world's end: {Time}/{Deadline}.\nWorld state:\n{World}");
                var action = currentAgent.PerformStep();
                if (Time + action.Cost > Deadline)
                {
                    Console.WriteLine($"{currentAgent.Id} decided to {action}, but there was not enough time for {currentAgent.Id} to finish his action.");
                    break;
                }
                action.Approve();
                Time += action.Cost;
                i = (i + 1) % Agents.Count;
                Thread.Sleep(2000);
                Console.WriteLine();
            }

            Console.WriteLine("\nALL YOUR BASE ARE BELONG TO US.");
        }
    }
}
