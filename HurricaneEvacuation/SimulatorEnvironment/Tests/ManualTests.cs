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
        private const string originalExample =
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

        private const string largerExample =
            "#V 9               \n" +

            "#E 1 2 W1          \n" + "#E 1 3 W5          \n" + "#E 1 4 W2          \n" +
            "#E 2 3 W1          \n" + "#E 2 5 W2          \n" +
            "#E 3 5 W5          \n" + "#E 3 4 W1          \n" +
            "#E 4 5 W1          \n" +
            "#E 5 6 W1          \n" + "#E 5 8 W1          \n" +
            "#E 8 7 W1          \n" + "#E 8 9 W5          \n" +
            "#E 7 9 W1          \n" +

            "#V 2 P 3           \n" +
            "#V 4 P 3           \n" +
            "#V 6 P 5           \n" +
            "#V 8 P 1           \n" +
            "#V 9 S             \n" +
            "#D 68             ";

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

        public void TestAStarLargerGraph()
        {
            (g, d) = new GraphParser().CreateGraphFromString(largerExample);
            k = 1;
            f = -1;
            a = new List<IAgent>();
            s = new CodeSettings(g, a, d, k, f);

            a.Add(new AStarAgent(1, s, g.Vertices[0]));

            var sim = new Simulator(s);
            sim.Start();
            Console.ReadLine();
        }

        public void TestAStarVandal()
        {
            (g, d) = new GraphParser().CreateGraphFromString(largerExample);
            k = 1;
            f = -1;
            a = new List<IAgent>();
            s = new CodeSettings(g, a, d, k, f);

            a.Add(new VandalAgent(1, s, g.Vertices[2], 0));
            a.Add(new AStarAgent(2, s, g.Vertices[0]));

            var sim = new Simulator(s);
            sim.Start();
            Console.ReadLine();
        }
    }
}
