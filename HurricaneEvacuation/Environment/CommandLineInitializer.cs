using System;
using System.Collections.Generic;
using HurricaneEvacuation.Agents;
using HurricaneEvacuation.GraphComponents;

namespace HurricaneEvacuation.Environment
{
    internal class CommandLineInitializer
    {
        public IState InitialState;
        private readonly AgentsFactory factory;
        public CommandLineInitializer(string graphText)
        {
            factory = new AgentsFactory();
            InitializeConstants();
            var graph = CreateGraph(graphText);
            var agents = CreateAgents(graph);

            InitialState = new State(graph, agents);
        }

        private void InitializeConstants()
        {
            int deadline;
            do
            {
                Console.WriteLine("Enter deadline, an integer > 0.");
            } while (!int.TryParse(Console.ReadLine(), out deadline) || deadline <= 0);

            double slowDown;
            do
            {
                Console.WriteLine("Enter slow-down parameter between 0-1.");
            } while (!double.TryParse(Console.ReadLine(), out slowDown) || !(0 <= slowDown && slowDown <= 1));

            int weightConstant;
            do
            {
                Console.WriteLine("Enter a negative weight constant integer.");
            } while (!int.TryParse(Console.ReadLine(), out weightConstant) || weightConstant >= 0);

            int cutoff;
            do
            {
                Console.WriteLine("Enter a cutoff limit, integer > 0.");
            } while (!int.TryParse(Console.ReadLine(), out cutoff) || cutoff <= 0);

            Constants.Initialize(deadline, slowDown, weightConstant, cutoff);
        }

        public IGraph CreateGraph(string text)
        {
            var parser = new GraphParser();
            return parser.CreateGraphFromString(text);
        }

        public List<IAgent> CreateAgents(IGraph graph)
        {
            int numOfAgents;
            do
            {
                Console.WriteLine("Please specify: <numOfAgents>");
            } while (!int.TryParse(Console.ReadLine(), out numOfAgents));

            Console.WriteLine(factory.MapToString());

            Console.WriteLine("Please specify: <agentType>;<vertexId> (also add ';<initialDelay>' for vandal agents or ';<maximumExpands> for RTA* agents)");
            var agents = new List<IAgent>();
            for (var agentId = 0; agentId < numOfAgents;)
            {
                var parts = Console.ReadLine()?.Split(';');
                var optional = 0;
                if (parts == null || parts.Length < 2 ||
                    !int.TryParse(parts[0], out var agentType) || !int.TryParse(parts[1], out var vertexId) ||
                    ((agentType == 2 || agentType == 5) && parts.Length > 2 && !int.TryParse(parts[2], out optional)))
                {
                    Console.WriteLine("Wrong parameters.");
                    continue;
                }

                var agent = factory.CreateAgent(agentType, vertexId, optional);

                agents.Add(agent);
                agentId++;
            }

            return agents;
        }
    }
}
