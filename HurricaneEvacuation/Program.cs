using System;
using System.IO;
using HurricaneEvacuation.Environment;
using HurricaneEvacuation.Tests;

namespace HurricaneEvacuation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            /*var graph = File.ReadAllText(args[1]);

            var initializer = new CommandLineInitializer(graph);
            var simulator = new Simulator(initializer.InitialState);
            simulator.Start();*/

            ManualTests m = new ManualTests();
            m.MultiAgentNoOp();
        }
    }
}
