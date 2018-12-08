using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HurricaneEvacuation.Agents.Basic_Agents;
using HurricaneEvacuation.Agents.Multi_Agents;
using HurricaneEvacuation.Agents.Search_Agents;

namespace HurricaneEvacuation.Agents
{
    internal class AgentsFactory
    {
        private int id;
        private int initialPosition;
        private int initialDelay;

        private Tuple<string, Func<IAgent>>[] agentsMapper;

        public AgentsFactory()
        {
            id = 0;

            agentsMapper = new[]
            {
                Tuple.Create<string, Func<IAgent>>(typeof(HumanAgent).Name, CreateHumanAgent),
                Tuple.Create<string, Func<IAgent>>(typeof(GreedyAgent).Name, CreateGreedyAgent),
                Tuple.Create<string, Func<IAgent>>(typeof(VandalAgent).Name, CreateVandalAgent),
                Tuple.Create<string, Func<IAgent>>(typeof(GreedySearchAgent).Name, CreateGreedyAgent),
                Tuple.Create<string, Func<IAgent>>(typeof(AStarAgent).Name, CreateAStarAgent),
                Tuple.Create<string, Func<IAgent>>(typeof(RTAStarAgent).Name, CreateRtaStarAgent),
                Tuple.Create<string, Func<IAgent>>(typeof(AdversarialAgent).Name, CreateAdversarialAgent),
                Tuple.Create<string, Func<IAgent>>(typeof(SemiCoOpAgent).Name, CreateSemiCoOpAgent),
                Tuple.Create<string, Func<IAgent>>(typeof(CoOpAgent).Name, CreateCoOpAgent),
            };
        }

        public IAgent CreateAgent(int agentType, int position)
        {
            initialPosition = position;
            return agentsMapper[agentType].Item2.Invoke();
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

        private IAgent CreateAdversarialAgent()
        {
            return new AdversarialAgent(id++, initialPosition);
        }

        private IAgent CreateSemiCoOpAgent()
        {
            return new SemiCoOpAgent(id++, initialPosition);
        }

        private IAgent CreateCoOpAgent()
        {
            return new CoOpAgent(id++, initialPosition);
        }

        public string MapToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < agentsMapper.Length; i++)
            {
                sb.Append($"{i}. {agentsMapper[i].Item1}\n");
            }

            return sb.ToString();
        }
    }
}
