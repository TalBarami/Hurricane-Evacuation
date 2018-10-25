using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment;
using HurricaneEvacuation.SimulatorEnvironment.Exceptions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;

namespace HurricaneEvacuation
{
    internal class Simulator
    {
        private IGraph World { get; set; }
        private IList<IAgent> Agents { get; set; }
        private int Deadline { get; set; }
        private int Time { get; set; }

        public Simulator()
        {
        }

        public void Initialize(string text)
        {
            CreateGraph(text);
            CreateAgents();
        }

        public void Start()
        {
            while (Time < Deadline)
            {
                Console.WriteLine($"\nStarting new round, time to world's end: {Time}/{Deadline}");
                foreach (var agent in Agents)
                {
                    agent.PerformStep(World);
                    System.Threading.Thread.Sleep(1000);
                }
                System.Threading.Thread.Sleep(1000);
                Time++;
            }

            Console.WriteLine("ALL YOUR BASE ARE BELONG TO US.");
        }

        public void CreateGraph(string text)
        {
            var parser = new GraphParser();
            var (graph, deadline) = parser.CreateGraphFromString(text);
            Deadline = deadline;
            World = graph;
            Time = 0;
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
                    !BuildAgent(agentId, agentType, World.Vertices.First(v => v.Id == vertexId), out var agent))
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
