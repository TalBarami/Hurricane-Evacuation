using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.AI_Agents
{
    internal class GreedySearchAgent : AbstractAiAgent
    {
        public GreedySearchAgent(int id, ISettings settings, IVertex position) : base(id, settings, position)
        {
            HeuristicFunction = new UnreachablePeopleFunction();
        }

        protected override IAction PlayNext(double time)
        {
            var hMapper = Position.ValidEdges().Select(e =>
            {
                var possibleTraverse = new Traverse(this, e, Settings.SlowDown);
                var hValue = HeuristicFunction.Value(Settings.Graph, possibleTraverse.Destination,
                    time + possibleTraverse.Cost, Settings.Deadline);
                return (possibleTraverse, hValue);
            }).ToList();

            return hMapper.Aggregate(hMapper[0], (minPair, newPair) =>
                newPair.Item2 < minPair.Item2 ? newPair : minPair).Item1;

        }
    }
}
