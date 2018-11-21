using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Environment;
using HurricaneEvacuation.GraphComponents.Vertices;

namespace HurricaneEvacuation.Agents
{
    public interface IAgent
    {
        int Id { get; }
        int Position { get; set; }
        double Score { get; }
        IAction NextStep(IState oldState);
        string Name { get; }
        double CalculateWeight(int edgeWeight);
        string Visit(IVertex destination);
        IAgent Clone();
    }
}
