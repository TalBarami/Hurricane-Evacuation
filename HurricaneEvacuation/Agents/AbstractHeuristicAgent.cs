using HurricaneEvacuation.Actions;

namespace HurricaneEvacuation.Agents
{
    abstract class AbstractHeuristicAgent : AbstractHelpfulAgent
    {
        protected AbstractHeuristicAgent(int id, int position) : base(id, position)
        {
        }

        protected AbstractHeuristicAgent(int id, int position, int passengers, int peopleSaved) : base(id, position, passengers, peopleSaved)
        {
        }
    }
}
