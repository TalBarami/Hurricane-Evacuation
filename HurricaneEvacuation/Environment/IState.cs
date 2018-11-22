using System.Collections.Generic;
using HurricaneEvacuation.Agents;
using HurricaneEvacuation.Agents.Basic_Agents;
using HurricaneEvacuation.Agents.Multi_Agents;
using HurricaneEvacuation.Agents.Search_Agents;
using HurricaneEvacuation.GraphComponents;
using HurricaneEvacuation.GraphComponents.Vertices;

namespace HurricaneEvacuation.Environment
{
    internal interface IState
    {
        double Time { get; set; }
        bool GoalState { get; }
        IGraph Graph { get; }
        List<ShelterVertex> ShelterVertices { get; }
        List<EvacuationVertex> EvacuationVertices { get; }

        List<IAgent> Agents { get; }
        int CurrentAgent { get; set; }
        List<VandalAgent> VandalAgents { get; }
        List<AbstractHelpfulAgent> HelpfulAgents { get; }
        List<AbstractSearchAgent> SearchAgents { get; }
        List<AbstractMultiAgent> MultiAgents { get; }
        void UpdateAgent(IAgent agent);

        IState Clone();
    }
}
