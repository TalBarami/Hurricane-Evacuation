using System.Collections.Generic;
using HurricaneEvacuation.Agents;
using HurricaneEvacuation.Agents.AI_Agents;
using HurricaneEvacuation.Agents.Basic_Agents;
using HurricaneEvacuation.Agents.Multi_Agents;
using HurricaneEvacuation.Environment;
using HurricaneEvacuation.GraphComponents;

namespace HurricaneEvacuation.Tests
{
    internal class ManualTests
    {
        private const string originalExample =
            "#V 4    ; number of vertices n in graph (from 1 to n)\n" +
            "#E 0 1 W1                 ; Edge from vertex 1 to vertex 2, weight 1\n" +
            "#E 2 3 W1                 ; Edge from vertex 3 to vertex 4, weight 1\n" +
            "#E 1 2 W1                 ; Edge from vertex 2 to vertex 3, weight 1\n" +
            "#E 0 2 W5                 ; Edge from vertex 1 to vertex 3, weight 4\n" +
            "#E 1 3 W5                 ; Edge from vertex 2 to vertex 4, weight 5\n" +
            "#V 1 P 1                  ; Vertex 2 initially contains 1 person to be rescued\n" +
            "#V 0 S                    ; Vertex 1 contains a hurricane shelter (a \"goal vertex\" - there may be more than one)\n" +
            "#V 3 P 2                  ; Vertex 4 initially contains 2 persons to be rescued";

        private const string largerExample =
            "#V 9               \n" +

            "#E 0 1 W1          \n" + "#E 0 2 W5          \n" + "#E 0 3 W2          \n" +
            "#E 1 2 W1          \n" + "#E 1 4 W2          \n" +
            "#E 2 4 W5          \n" + "#E 2 3 W1          \n" +
            "#E 3 4 W1          \n" +
            "#E 4 5 W1          \n" + "#E 4 7 W1          \n" +
            "#E 7 6 W1          \n" + "#E 7 8 W5          \n" +
            "#E 6 8 W1          \n" +

            "#V 1 P 3           \n" +
            "#V 3 P 3           \n" +
            "#V 5 P 5           \n" +
            "#V 7 P 1           \n" +
            "#V 8 S             ";

        private const string classExample =
            "#V 4               \n" +
            "#E 0 1 W1          \n" +
            "#E 0 2 W2          \n" +
            "#E 1 2 W5          \n" +
            "#E 1 3 W1          \n" +
            "#E 2 3 W1          \n" +
            "#V 2 S             \n" +
            "#V 1 P 3           \n" +
            "#V 3 P 1           ";

        private const string astarVandal =
            "#V 4               \n" +
            "#E 0 1 W1          \n" +
            "#E 1 2 W1          \n" +
            "#E 2 3 W1          \n" +
            "#E 0 3 W4          \n" +
            "#V 0 S             \n" +
            "#V 3 P 1";

        private const string astarVandal2 =
            "#V 3               \n" +
            "#E 0 1 W1          \n" +
            "#E 0 2 W3          \n" +
            "#V 0 S             \n" +
            "#V 1 P 1             \n" +
            "#V 2 P 1";

        private IList<IAgent> a;
        private IGraph g;

        public void BasicVandalTest()
        {
            g = new GraphParser().CreateGraphFromString(originalExample);
            Constants.Initialize(50, 1, -1, 3);
            
            var initialState = new State(g, new List<IAgent>()
            {
                new VandalAgent(0, 0, 1)
            });

            Simulator s = new Simulator(initialState);
            s.Start();
        }

        public void BasicHumanTest()
        {
            g = new GraphParser().CreateGraphFromString(originalExample);
            Constants.Initialize(50, 1, -1, 3);

            var initialState = new State(g, new List<IAgent>()
            {
                new HumanAgent(0, 0)
            });

            Simulator s = new Simulator(initialState);
            s.Start();
        }

        public void BasicGreedyTest()
        {
            g = new GraphParser().CreateGraphFromString(originalExample);
            Constants.Initialize(50, 1, -1, 3);

            var initialState = new State(g, new List<IAgent>()
            {
                new GreedyAgent(0, 0)
            });

            Simulator s = new Simulator(initialState);
            s.Start();
        }

        public void BasicGreedyVandal()
        {
            g = new GraphParser().CreateGraphFromString(originalExample);
            Constants.Initialize(50, 1, -1, 3);

            var initialState = new State(g, new List<IAgent>()
            {
                new GreedyAgent(0, 0),
                new VandalAgent(1, 3, 4)
            });

            Simulator s = new Simulator(initialState);
            s.Start();
        }

        public void BasicAStarTest()
        {
            g = new GraphParser().CreateGraphFromString(originalExample);
            Constants.Initialize(15, 1, -1, 3);

            var initialState = new State(g, new List<IAgent>()
            {
                new AStarAgent(0, 0)
            });

            Simulator s = new Simulator(initialState);
            s.Start();
        }

        public void BasicGreedySearchTest()
        {
            g = new GraphParser().CreateGraphFromString(originalExample);
            Constants.Initialize(15, 1, -1, 3);

            var initialState = new State(g, new List<IAgent>()
            {
                new GreedySearchAgent(0, 0)
            });

            Simulator s = new Simulator(initialState);
            s.Start();
        }

        public void ClassGreedySearchTest()
        {
            g = new GraphParser().CreateGraphFromString(classExample);
            Constants.Initialize(5, 1, -1, 3);

            var initialState = new State(g, new List<IAgent>()
            {
                new GreedySearchAgent(0, 0)
            });

            Simulator s = new Simulator(initialState);
            s.Start();
        }

        public void BasicRTAStarTest()
        {
            g = new GraphParser().CreateGraphFromString(originalExample);
            Constants.Initialize(15, 1, -1, 3);

            var initialState = new State(g, new List<IAgent>()
            {
                new RTAStarAgent(0, 0, 3)
            });

            Simulator s = new Simulator(initialState);
            s.Start();
        }

        public void ComplexGreedySearchTest()
        {
            g = new GraphParser().CreateGraphFromString(largerExample);
            Constants.Initialize(100, 1, -10, 3);

            var initialState = new State(g, new List<IAgent>()
            {
                new GreedySearchAgent(0, 0)
            });

            Simulator s = new Simulator(initialState);
            s.Start();
        }

        public void ComplexAStarTest()
        {
            g = new GraphParser().CreateGraphFromString(largerExample);
            Constants.Initialize(60, 1, -10, 3);

            var initialState = new State(g, new List<IAgent>()
            {
                new AStarAgent(0, 0)
            });

            Simulator s = new Simulator(initialState);
            s.Start();
        }

        public void ComplexRtaStarTest()
        {
            g = new GraphParser().CreateGraphFromString(largerExample);
            Constants.Initialize(100, 1, -10, 3);

            var initialState = new State(g, new List<IAgent>()
            {
                new RTAStarAgent(0, 0, 3)
            });

            Simulator s = new Simulator(initialState);
            s.Start();
        }

        public void ComplexRtaStarVandalTest()
        {
            g = new GraphParser().CreateGraphFromString(largerExample);
            Constants.Initialize(100, 1, -10, 3);

            var initialState = new State(g, new List<IAgent>()
            {
                new RTAStarAgent(0, 0, 3),
                new VandalAgent(1, 0, 2)
            });

            Simulator s = new Simulator(initialState);
            s.Start();
        }

        public void ComplexAStarVandalTest()
        {
            g = new GraphParser().CreateGraphFromString(largerExample);
            Constants.Initialize(100, 1, -10, 3);

            var initialState = new State(g, new List<IAgent>()
            {
                new AStarAgent(0, 0),
                new VandalAgent(1, 0, 2)
            });

            Simulator s = new Simulator(initialState);
            s.Start();
        }

        public void BasicAStarVandalTest()
        {
            g = new GraphParser().CreateGraphFromString(astarVandal);
            Constants.Initialize(14, 1, -10, 3);

            var initialState = new State(g, new List<IAgent>()
            {
                new AStarAgent(0, 0),
                new VandalAgent(1, 2, 0)
            });

            Simulator s = new Simulator(initialState);
            s.Start();
        }

        public void LargeThreeAgentsTest()
        {
            g = new GraphParser().CreateGraphFromString(largerExample);
            Constants.Initialize(14, 1, -10, 3);

            var initialState = new State(g, new List<IAgent>()
            {
                new HumanAgent(0, 0),
                new VandalAgent(1, 5, 0),
                new GreedyAgent(2, 3)
            });

            Simulator s = new Simulator(initialState);
            s.Start();
        }

        public void AStarVandal2()
        {
            g = new GraphParser().CreateGraphFromString(astarVandal2);
            Constants.Initialize(16, 1, -10, 3);

            var initialState = new State(g, new List<IAgent>()
            {
                new AStarAgent(0, 0),
                new VandalAgent(1, 2, 1)
            });

            Simulator s = new Simulator(initialState);
            s.Start();
        }

        public void MultiAgentBasic()
        {
            g = new GraphParser().CreateGraphFromString(originalExample);
            Constants.Initialize(10, 1, -10, 4);

            var initialState = new State(g, new List<IAgent>()
            {
                new AdversarialAgent(0, 0),
                new AdversarialAgent(1, 0)
            });

            Simulator s = new Simulator(initialState);
            s.Start();
        }
    }
}
