using System;
using HurricaneEvacuation.Actions;
using HurricaneEvacuation.Agents.AI_Agents;
using HurricaneEvacuation.Environment;

namespace HurricaneEvacuation.Agents.Multi_Agents
{
    class SemiCoOpAgent : AbstractMultiAgent
    {
        public SemiCoOpAgent(int id, int position) : base(id, position)
        {
        }

        public SemiCoOpAgent(int id, int position, int passengers, int peopleSaved) : base(id, position, passengers, peopleSaved)
        {
        }

        public SemiCoOpAgent(SemiCoOpAgent other) : this(other.Id, other.Position, other.Passengers,
            other.PeopleSaved)
        {
        }

        public override IAgent Clone()
        {
            return new SemiCoOpAgent(this);
        }

        public override HeuristicResult Heuristic(IAction action)
        {
            throw new NotImplementedException();
        }

        public override double MultiScore(IState state)
        {
            return Score;
        }
    }
}
