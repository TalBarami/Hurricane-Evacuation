using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment.Exceptions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Settings
{
    internal class ConsoleSettings : AbstractSettings
    {
        public ConsoleSettings()
        {
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
            Console.WriteLine("Please specify: <agentType>;<vertexId>");
            var agents = new List<IAgent>();
            for (var agentId = 0; agentId < numOfAgents;)
            {
                var parts = Console.ReadLine()?.Split(';');
                if (parts == null || parts.Length < 2 ||
                    !int.TryParse(parts[0], out var agentType) || !int.TryParse(parts[1], out var vertexId) ||
                    !BuildAgent(agentId, agentType, Graph.Vertices.First(v => v.Id == vertexId), out var agent))
                {
                    Console.WriteLine("Wrong parameters.");
                    continue;
                }

                agents.Add(agent);
                agentId++;
            }

            Agents = agents;
        }

        private static bool BuildAgent(int agentId, int agentType, IVertex v, out IAgent agent)
        {
            switch (agentType)
            {
                case 1:
                    agent = new HumanAgent(agentId, v);
                    break;
                case 2:
                    agent = new GreedyAgent(agentId, v);
                    break;
                case 3:
                    {
                        Console.WriteLine("Please specify <initialDelay> for the Vandal Agent.");
                        if (!int.TryParse(Console.ReadLine(), out var initialDelay))
                        {
                            agent = null;
                            return false;
                        }
                        agent = new VandalAgent(agentId, v, initialDelay);
                        break;
                    }
                default:
                    throw new InvalidAgentIdException($"Unidentified Id: {agentType}");
            }

            return true;
        }
    }
}
