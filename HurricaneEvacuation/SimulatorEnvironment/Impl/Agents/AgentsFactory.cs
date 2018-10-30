using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.AI_Agents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.NormalAgents;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents
{
    class AgentsFactory
    {
        private int id;
        private readonly ISettings settings;
        private IVertex initialPosition;
        private int initialDelay;

        private Dictionary<int, Func<IAgent>> agentsMapper;

        public AgentsFactory(ISettings settings)
        {
            id = 0;
            this.settings = settings;

            agentsMapper = new Dictionary<int, Func<IAgent>>()
            {
                {0, CreateHumanAgent},
                {1, CreateGreedyAgent },
                {2, CreateVandalAgent },
                {3, CreateGreedySearchAgent },
                {4, CreateAStarAgent },
                {5, CreateRtaStarAgent }
            };
        }

        public IAgent CreateAgent(int agentType, IVertex position)
        {
            initialPosition = position;
            return agentsMapper[agentType].Invoke();
        }

        public IAgent CreateAgent(int agentType, IVertex position, int delay)
        {
            initialDelay = delay;
            return CreateAgent(agentType, initialPosition);
        }

        private IAgent CreateHumanAgent()
        {
            return new HumanAgent(id++, settings, initialPosition);
        }

        private IAgent CreateGreedyAgent()
        {
            return new GreedyAgent(id++, settings, initialPosition);
        }

        private IAgent CreateVandalAgent()
        {
            return new VandalAgent(id++, settings, initialPosition, initialDelay);
        }

        private IAgent CreateGreedySearchAgent()
        {
            return new GreedySearchAgent(id++, settings, initialPosition);
        }

        private IAgent CreateAStarAgent()
        {
            return new AStarAgent(id++, settings, initialPosition);
        }

        private IAgent CreateRtaStarAgent()
        {
            return new RtaStarAgent(id++, settings, initialPosition);
        }
    }
}
