using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.AI_Agents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.NormalAgents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Settings;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var s = "#V 6    ; number of vertices n in graph (from 1 to n)\n" +
                    "#E 1 2 W1                 ; Edge from vertex 1 to vertex 2, weight 1\n" +
                    "#E 3 4 W1                 ; Edge from vertex 3 to vertex 4, weight 1\n" +
                    "#E 2 3 W1                 ; Edge from vertex 2 to vertex 3, weight 1\n" +
                    "#E 1 3 W4                 ; Edge from vertex 1 to vertex 3, weight 4\n" +
                    "#E 2 4 W5                 ; Edge from vertex 2 to vertex 4, weight 5\n" +
                    "#E 5 6 W20\n" +
                    //"#E 1 6 W10\n" + 
                    "#V 2 P 1                  ; Vertex 2 initially contains 1 person to be rescued\n" +
                    "#V 1 S                    ; Vertex 1 contains a hurricane shelter (a \"goal vertex\" - there may be more than one)\n" +
                    "#V 4 P 2                  ; Vertex 4 initially contains 2 persons to be rescued\n" +
                    "#V 5 S\n" +
                    "#V 6 P 15\n" +
                    "#D 20                     ; Deadline is at time 10";
            var (graph, deadline) = new GraphParser().CreateGraphFromString(s);

            IList<IAgent> agents = new List<IAgent>();

            var settings = new CodeSettings(graph, agents, deadline, .75, -1);

            //agents.Add(new GreedyAgent(1, settings, graph.Vertices[0]));
            //agents.Add(new VandalAgent(2, settings, graph.Vertices[3], 2));
            agents.Add(new GreedySearchAgent(1, settings, graph.Vertices[0]));

            var sim = new Simulator(settings);
            sim.Start();
            Console.ReadLine();
        }
    }
}
