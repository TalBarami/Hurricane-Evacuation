using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using HurricaneEvacuation.SimulatorEnvironment.Exceptions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.AI_Agents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.NormalAgents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Settings
{
    internal class ConsoleSettings : AbstractSettings
    {
        private AgentsFactory factory;
        public ConsoleSettings()
        {
            factory = new AgentsFactory(this);
            CreateGraph("");
            CreateAgents();
        }

        public void CreateGraph(string text)
        {
            var parser = new GraphParser();
            var (graph, deadline) = parser.CreateGraphFromString(text);
            double slowDown;
            do
            {
                Console.WriteLine("Enter slow-down parameter between 0-1.");
            } while (!double.TryParse(Console.ReadLine(), out slowDown) || !(0 <= slowDown && slowDown <= 1));
            Deadline = deadline;
            Graph = graph;
            SlowDown = slowDown;
        }

        public void CreateAgents()
        {
            Console.WriteLine("Please specify: <numOfAgents>");
            if (!int.TryParse(Console.ReadLine(), out var numOfAgents))
            {
                Console.WriteLine("Bye...");
                return;
            }
            Console.WriteLine("Please specify: <agentType>;<vertexId> (also add ';<initialDelay>' for vandal agents)");
            var agents = new List<IAgent>();
            for (var agentId = 0; agentId < numOfAgents;)
            {
                var parts = Console.ReadLine()?.Split(';');
                var initialDelay = 0;
                if (parts == null || parts.Length < 2 ||
                    !int.TryParse(parts[0], out var agentType) || !int.TryParse(parts[1], out var vertexId) ||
                    (agentType == 2 && !int.TryParse(parts[2], out initialDelay)))
                {
                    Console.WriteLine("Wrong parameters.");
                    continue;
                }

                var agent = factory.CreateAgent(agentType, Graph.Vertices.First(v => v.Id == vertexId), initialDelay);

                agents.Add(agent);
                agentId++;
            }

            Agents = agents;
        }
    }
}
