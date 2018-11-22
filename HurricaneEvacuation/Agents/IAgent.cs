using System.Collections.Generic;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Environment;
using HurricaneEvacuation.GraphComponents.Vertices;

namespace HurricaneEvacuation.Agents
{
    internal interface IAgent
    {
        int Id { get; }
        int Position { get; set; }
        double Score { get; }
        IAction NextStep(IState oldState);
        List<IAction> PossibleMoves(IState state);
        string Name { get; }
        double CalculateWeight(int edgeWeight);
        string Visit(IVertex destination);
        IAgent Clone();
    }
}
