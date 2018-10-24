using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment;
using HurricaneEvacuation.SimulatorEnvironment.Exceptions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents;
using HurricaneEvacuation.Utils;

namespace HurricaneEvacuation
{
    class Simulator
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
            var i = 0;
            while (Time < Deadline)
            {
                var action = Agents[i].PerformStep(World);



                i++;
            }
        }

        public void CreateGraph(string text)
        {
            var parser = new GraphParser();
            var (graph, deadline, src) = parser.CreateGraphFromString(text);
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
            Console.WriteLine("Please specify: <agentId>;<vertexId>");
            var agents = new List<IAgent>();
            for (var i = 0; i < numOfAgents;)
            {
                var parts = Console.ReadLine()?.Split(';');
                if (parts == null || parts.Length < 2 ||
                    !int.TryParse(parts[0], out var agentId) || !int.TryParse(parts[1], out var vertexId) ||
                    !BuildAgent(agentId, World.Vertices.First(v => v.Id == vertexId), out var agent))
                {
                    Console.WriteLine("Wrong parameters.");
                    continue;
                }

                agents.Add(agent);
                i++;
            }

            Agents = agents;
        }

        private static bool BuildAgent(int agentId, IVertex v, out IAgent agent)
        {
            switch (agentId)
            {
                case 1:
                    agent = new HumanAgent(v);
                    break;
                case 2:
                    agent = new GreedyAgent(v);
                    break;
                case 3:
                {
                    Console.WriteLine("Please specify <initialDelay> for the Vandal Agent.");
                    if (!int.TryParse(Console.ReadLine(), out var initialDelay))
                    {
                        agent = null;
                        return false;
                    }
                    agent = new VandalAgent(v, initialDelay);
                    break;
                }
                default:
                    throw new InvalidAgentIdException($"Unidentified Id: {agentId}");
            }

            return true;
        }
    }
}
