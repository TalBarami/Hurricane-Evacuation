using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Environment;
using HurricaneEvacuation.GraphComponents;
using HurricaneEvacuation.GraphComponents.Vertices;

namespace HurricaneEvacuation.Agents.Basic_Agents
{
    internal class GreedyAgent : AbstractHelpfulAgent
    {
        public GreedyAgent(int id, int position) : base(id, position)
        {
        }

        public GreedyAgent(GreedyAgent other) : base(other.Id, other.Position, other.Passengers, other.PeopleSaved)
        {
        }

        public override IAgent Clone()
        {
            return new GreedyAgent(this);
        }

        protected override IAction CalculateMove(IState oldState)
        {
            var newState = oldState.Clone();
            var newAgent = new GreedyAgent(this);
            newState.UpdateAgent(newAgent);

            var position = newState.Graph.Vertex(Position);
            var paths = newState.Graph.Dijkstra(position);

            var selectedPath = Passengers > 0 ? FindShelter(paths, oldState) : FindPeople(paths, oldState);

            if (selectedPath.Count <= 1)
            {
                return new NoOp(oldState, newState, Id);
            }
            
            return new Traverse(oldState, newState, Id, selectedPath.Vertices[1].Id);
        }

        private Path FindShelter(IReadOnlyList<Path> paths, IState state)
        {
            return Path.ShortestTraversePath(paths.Where(p => p.Last is ShelterVertex).ToList(), state, Id);
        }

        private Path FindPeople(IReadOnlyList<Path> paths, IState state)
        {
            return Path.ShortestTraversePath(paths.Where(p => p.Last is EvacuationVertex).ToList(), state, Id);
        }
    }
}
