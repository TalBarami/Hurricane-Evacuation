﻿using System;
using System.Collections.Generic;
using System.Linq;
using HurricaneEvacuation.SimulatorEnvironment.Impl.Actions;
using HurricaneEvacuation.SimulatorEnvironment.Impl.GraphComponents;
using HurricaneEvacuation.SimulatorEnvironment.Impl.HeuristicFunctions;
using HurricaneEvacuation.SimulatorEnvironment.Utils;

namespace HurricaneEvacuation.SimulatorEnvironment.Impl.Agents.AI_Agents
{
    internal class AStarAgent : AbstractAiAgent
    {
        private double travelTime;
        public AStarAgent(int id, ISettings settings, IVertex position) : base(id, settings, position)
        {
            travelTime = 0;
            HeuristicFunction = new UnreachablePeopleFunction();
        }

        protected override IAction PlayNext(double time)
        {
            /*var hValues = GetHValues(Position, Passengers, time);
            foreach (var hr in hValues)
            {
                hr.TravelTime = travelTime;
            }
            var minimal = hValues.Where(hResult => Math.Abs(hResult.FValue - hValues.Min(h => h.FValue)) < Tolerance).ToList();
            Console.WriteLine($"\tF Returned:\n\t\t{hValues.ListToString().Replace(" ; ", "\n\t\t")}");
            var selected = PickBest(minimal).Action;
            travelTime += selected.Cost;
            return selected;*/
            return Expand(time);
        }

        public IAction Expand(double time)
        {
            var initial = new HeuristicResult(new Traverse(this, new Edge(null, null, -1), Position, Passengers, Settings.SlowDown),
                    double.MaxValue, Passengers, time, Settings.Deadline)
                { TravelTime = travelTime };
            var root = new ExpandNode(initial);
            var node = root;
            var possibleResults = new List<HeuristicResult>();
            var current = initial;
            var mapper = new Dictionary<HeuristicResult, ExpandNode>() {{current, node}};

            double expands = 0;

            while (!current.GoalReached)
            {
                var hValues = GetHValues(current.Action.Destination, node.Visited, current.Passengers, current.Time);
                hValues.ForEach(hr => hr.TravelTime = current.TravelTime + hr.Action.Cost);
                foreach (var hv in hValues)
                {
                    mapper.Add(hv, node.AddChild(hv));
                    possibleResults.Add(hv);
                }

                current = possibleResults.SelectMinimal();
                node = mapper[current];
                possibleResults.Remove(current);

                expands++;
                if (expands <= Math.Pow(Settings.Graph.Edges.Count, 2)) continue;
                var results = root.children.Select(child => child.result).ToList();
                var minimal = results.Where(hResult => Math.Abs(hResult.FValue - results.Min(h => h.FValue)) < Tolerance).ToList();
                Console.WriteLine($"\tF Returned:\n\t\t{results.ListToString().Replace(" ; ", "\n\t\t")}");
                var selected = PickBest(minimal).Action;
                travelTime += selected.Cost;
                return selected;
            }

            var path = new Path(node.result.Action.Destination, null, 0);
            while (node.root.result != initial)
            {
                path = new Path(node.root.result.Action.Destination, path, 0);
                node = node.root;
            }
            Console.WriteLine(path);

            return node.result.Action;
        }
    }
}
