using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment;
using HurricaneEvacuation.SimulatorEnvironment.Exceptions;
using HurricaneEvacuation.SimulatorEnvironment.Impl;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Settings;

namespace HurricaneEvacuation
{
    internal class Simulator
    {
        private IGraph world { get; set; }
        private IList<IAgent> agents { get; set; }
        private int deadline { get; set; }
        private double time { get; set; }

        public Simulator()
        {
            Initialize();
        }

        private void Initialize()
        {
            var settings = SettingsSingleton.Instance;
            world = settings.Graph;
            agents = settings.Agents;
            deadline = settings.Deadline;
            time = 0;
        }

        public void Start()
        {
            var i = 0;
            while (time < deadline)
            {
                var currentAgent = agents[i];
                Console.WriteLine($"Time to world's end: {time}/{deadline}.\nWorld state:\n{world}");
                var action = currentAgent.PerformStep();
                time += action.Cost;
                i = (i + 1) % agents.Count;
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine();
            }

            Console.WriteLine("\nALL YOUR BASE ARE BELONG TO US.");
        }
    }
}
