using System.Collections.Generic;
using System.Linq;
using System.Text;
using HurricaneEvacuation.Agents;
using HurricaneEvacuation.Agents.Basic_Agents;
using HurricaneEvacuation.Agents.Multi_Agents;
using HurricaneEvacuation.Agents.Search_Agents;
using HurricaneEvacuation.GraphComponents;
using HurricaneEvacuation.GraphComponents.Vertices;

namespace HurricaneEvacuation.Environment
{
    internal class State : IState
    {
        public double Time { get; set; }
        public bool GoalState => Time > Constants.Deadline || (EvacuationVertices.Count == 0 && HelpfulAgents.All(agent => agent.Passengers == 0));
        public IGraph Graph { get; }
        public List<ShelterVertex> ShelterVertices => Graph.Vertices.OfType<ShelterVertex>().ToList();
        public List<EvacuationVertex> EvacuationVertices => Graph.Vertices.OfType<EvacuationVertex>().Where(v => v.PeopleCount > 0).ToList();
        public List<IAgent> Agents { get; }
        public int CurrentAgent { get; set; }
        public List<VandalAgent> VandalAgents => Agents.OfType<VandalAgent>().ToList();
        public List<AbstractHelpfulAgent> HelpfulAgents => Agents.OfType<AbstractHelpfulAgent>().ToList();
        public List<AbstractSearchAgent> SearchAgents => Agents.OfType<AbstractSearchAgent>().ToList();
        public List<AbstractMultiAgent> MultiAgents => Agents.OfType<AbstractMultiAgent>().ToList();


        public State(double time, IGraph graph, List<IAgent> agents, int currentAgent)
        {
            Time = time;
            Graph = graph.Clone();
            Agents = agents.Select(a => a.Clone()).ToList();
            CurrentAgent = currentAgent;
        }
        public State(IGraph graph, List<IAgent> agents) : this(0, graph, agents, 0)
        {
        }

        public State(IState other) : this(other.Time, other.Graph, other.Agents, other.CurrentAgent)
        {
        }

        public void UpdateAgent(IAgent agent)
        {
            var current = Agents.First(a => a.Id == agent.Id);
            Agents[Agents.IndexOf(current)] = agent;
        }

        public IState Clone()
        {
            return new State(this);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"Time: {Time}/{Constants.Deadline}").Append("\n")
                .Append("Graph:").Append("\n")
                .Append(Graph).Append("\n")
                .Append("Agents:").Append("\n")
                .Append("\t").Append(string.Join("\n\t", Agents)).Append("\n")
                .Append($"Now playing: {Agents[CurrentAgent].Name}\n");

            return sb.ToString();
        }
    }
}
