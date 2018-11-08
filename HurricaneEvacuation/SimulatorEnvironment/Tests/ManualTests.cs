using System;
using System.Collections.Generic;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.AI_Agents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.NormalAgents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Settings;

namespace HurricaneEvacuation.SimulatorEnvironment.Tests
{
    class ManualTests
    {
        private string originalExample =
            "#V 4    ; number of vertices n in graph (from 1 to n)\n" +
            "#E 1 2 W1                 ; Edge from vertex 1 to vertex 2, weight 1\n" +
            "#E 3 4 W1                 ; Edge from vertex 3 to vertex 4, weight 1\n" +
            "#E 2 3 W1                 ; Edge from vertex 2 to vertex 3, weight 1\n" +
            "#E 1 3 W4                 ; Edge from vertex 1 to vertex 3, weight 4\n" +
            "#E 2 4 W5                 ; Edge from vertex 2 to vertex 4, weight 5\n" +
            "#V 2 P 1                  ; Vertex 2 initially contains 1 person to be rescued\n" +
            "#V 1 S                    ; Vertex 1 contains a hurricane shelter (a \"goal vertex\" - there may be more than one)\n" +
            "#V 4 P 2                  ; Vertex 4 initially contains 2 persons to be rescued\n" +
            "#D 16                     ; Deadline is at time 10";

        private IList<IAgent> a;
        private IGraph g;
        private int d, k, f;
        private ISettings s;

        public ManualTests()
        {

        }

        public void TestAStar()
        {
            (g, d) = new GraphParser().CreateGraphFromString(originalExample);
            k = 1;
            f = -1;
            a = new List<IAgent>();
            s = new CodeSettings(g, a, d, k, f);

            a.Add(new AStarAgent(1, s, g.Vertices[0]));

            var sim = new Simulator(s);
            sim.Start();
            Console.ReadLine();
        }

        public void TestRtaStar()
        {

            (g, d) = new GraphParser().CreateGraphFromString(originalExample);
            k = 1;
            f = -1;
            a = new List<IAgent>();
            s = new CodeSettings(g, a, d, k, f);

            a.Add(new RtaStarAgent(1, s, g.Vertices[0]));

            var sim = new Simulator(s);
            sim.Start();
            Console.ReadLine();
        }

        public void TestGreedyVandal()
        {
            (g, d) = new GraphParser().CreateGraphFromString(originalExample);
            k = 1;
            f = -1;
            a = new List<IAgent>();
            s = new CodeSettings(g, a, d, k, f);

            a.Add(new VandalAgent(2, s, g.Vertices[3], 4));
            a.Add(new GreedyAgent(1, s, g.Vertices[1]));;

            var sim = new Simulator(s);
            sim.Start();
            Console.ReadLine();
        }

        public void TestVandal()
        {
            (g, d) = new GraphParser().CreateGraphFromString(originalExample);
            d = 50;
            k = 1;
            f = -1;
            a = new List<IAgent>();
            s = new CodeSettings(g, a, d, k, f);

            a.Add(new VandalAgent(2, s, g.Vertices[3], 3));

            var sim = new Simulator(s);
            sim.Start();
            Console.ReadLine();
        }
    }
}
