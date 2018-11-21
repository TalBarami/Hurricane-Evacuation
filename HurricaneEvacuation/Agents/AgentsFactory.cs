using System;
using System.Collections.Generic;
using HurricaneEvacuation.Agents.AI_Agents;
using HurricaneEvacuation.Agents.Basic_Agents;

namespace HurricaneEvacuation.Agents
{
    internal class AgentsFactory
    {
        private int id;
        private int initialPosition;
        private int initialDelay;

        private Dictionary<int, Func<IAgent>> agentsMapper;

        public AgentsFactory()
        {
            id = 0;

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

        public IAgent CreateAgent(int agentType, int position)
        {
            initialPosition = position;
            return agentsMapper[agentType].Invoke();
        }

        public IAgent CreateAgent(int agentType, int position, int delay)
        {
            initialDelay = delay;
            return CreateAgent(agentType, position);
        }

        private IAgent CreateHumanAgent()
        {
            return new HumanAgent(id++, initialPosition);
        }

        private IAgent CreateGreedyAgent()
        {
            return new GreedyAgent(id++, initialPosition);
        }

        private IAgent CreateVandalAgent()
        {
            return new VandalAgent(id++, initialPosition, initialDelay);
        }

        private IAgent CreateGreedySearchAgent()
        {
            return new GreedySearchAgent(id++, initialPosition);
        }

        private IAgent CreateAStarAgent()
        {
            return new AStarAgent(id++, initialPosition);
        }

        private IAgent CreateRtaStarAgent()
        {
            return new RTAStarAgent(id++, initialPosition, 50);
        }
    }
}
